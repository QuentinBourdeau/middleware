using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using test.ProxyCacheRef;
using System.Device.Location;
using System.Runtime.CompilerServices;

namespace test
{
    public class ClientJCDecauxAPI
    {
        private readonly ProxyCacheClient proxy;
        private readonly Utils utils = new Utils();

        public ClientJCDecauxAPI(ProxyCacheClient proxyCacheClient)
        {
            proxy = proxyCacheClient;
        }

        public JCDStation retrieveClosestStation(GeoCoordinate position, List<JCDStation> stations)
        {

            double minDistance = Double.MaxValue;
            JCDStation closestStation = null;

            foreach (JCDStation station in stations)
            {
                GeoCoordinate stationGeo = new GeoCoordinate(station.position.latitude, station.position.longitude);
                double distance = position.GetDistanceTo(stationGeo);

                if (distance < minDistance)
                {
                    closestStation = station;
                    minDistance = distance;
                }
            }
            return closestStation;
        }

        public JCDStation retrieveClosestStationDeparture(GeoCoordinate position)
        {
            //JCDecauxItem is the generic and only type sent by the proxy server
            //so it needs to be deserialized to be used
            JCDecauxItem jCDecauxItem= proxy.getStationsList();
            List<JCDStation> stations = JsonSerializer.Deserialize<List<JCDStation>>(jCDecauxItem.response);
            return retrieveClosestStation(position, stations.Where(station => station.totalStands.availabilities.bikes != 0).ToList());
        }

        public JCDStation retrieveClosestStationArrival(GeoCoordinate position, JCDContract jCDContract)
        {
            JCDecauxItem jCDecauxItem = proxy.getStationsListWithContractName(jCDContract.name);
            List<JCDStation> stations = JsonSerializer.Deserialize<List<JCDStation>>(jCDecauxItem.response);
            return retrieveClosestStation(position, stations.Where(station => station.totalStands.availabilities.stands != 0).ToList());
        }

        public JCDContract contractFromChosenStation(JCDStation jCDStation)
        {
            JCDecauxItem jCDecauxItem = proxy.getContractsList();
            List<JCDContract> contracts = JsonSerializer.Deserialize<List<JCDContract>>(jCDecauxItem.response);
            utils.cleanContractsList(contracts);
            foreach (JCDContract contract in contracts)
            {
                if (contract.name == jCDStation.contractName)
                {
                    return contract;
                }
            }
            return null;
        }

        
    }
}
