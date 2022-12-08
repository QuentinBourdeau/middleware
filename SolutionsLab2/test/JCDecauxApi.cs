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
            /*foreach (JCDStation station in stations)
            {
                Console.WriteLine(station.name);
            }*/

            double minDistance = Double.MaxValue;
            JCDStation closestStation = null;

            foreach (JCDStation station in stations)
            {
                // Find the current station's position.
                GeoCoordinate stationGeo = new GeoCoordinate(station.position.latitude, station.position.longitude);
                // And compare its distance to the chosen one to see if it is closer than the current closest.
                double distance = position.GetDistanceTo(stationGeo);

                if (distance < minDistance)
                {
                    closestStation = station;
                    minDistance = distance;
                }
            }
            Console.WriteLine("Closest station is " + closestStation.name);
            return closestStation;
        }

        public JCDStation retrieveClosestStationDeparture(GeoCoordinate position)
        {
            JCDecauxItem jCDecauxItem= proxy.getStationsList();
            List<JCDStation> stations = JsonSerializer.Deserialize<List<JCDStation>>(jCDecauxItem.response);;
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
