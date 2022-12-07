using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using test.ProxyCacheRef;
using System.Device.Location;

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

        public JCDStation retrieveClosestStation(GeoCoordinate chosenStationGeo, List<JCDStation> stations)
        {

            double minDistance = Double.MaxValue;
            JCDStation closestStation = null;

            foreach (JCDStation station in stations)
            {
                // Find the current station's position.
                GeoCoordinate stationGeo = new GeoCoordinate(station.position.latitude, station.position.longitude);
                // And compare its distance to the chosen one to see if it is closer than the current closest.
                double distance = chosenStationGeo.GetDistanceTo(stationGeo);

                if (distance < minDistance)
                {
                    closestStation = station;
                    minDistance = distance;
                }
            }
            return closestStation;
        }

        public JCDStation retrieveClosestStationDeparture(GeoCoordinate position, string originCity, string destinationCity)
        {
            List<JCDContract> contracts = contractsContainingCities(originCity, destinationCity);
            List<JCDStation> stations = new List<JCDStation>();
            string stationTemp;
            foreach (JCDContract contract in contracts)
            {
                stationTemp = proxy.getStationsListWithContractName(contract.name).response;
                stations.Add(JsonSerializer.Deserialize<JCDStation>(stationTemp));

            }
            //string items = proxy.getStationsList().response;
            //List<JCDStation> stations = JsonSerializer.Deserialize<List<JCDStation>>(items);
            return retrieveClosestStation(position, stations.Where(station => station.totalStands.availabilities.bikes != 0).ToList());
        }

        public JCDStation retrieveClosestStationArrival(GeoCoordinate position, string originCity, string destinationCity)
        {
            List<JCDContract> contracts = contractsContainingCities(originCity, destinationCity);
            List<JCDStation> stations = new List<JCDStation>();
            string stationTemp;
            foreach (JCDContract contract in contracts)
            {
                stationTemp = proxy.getStationsListWithContractName(contract.name).response;
                stations.Add(JsonSerializer.Deserialize<JCDStation>(stationTemp));

            }
            //string items = proxy.getStationsList().response;
            //stations = JsonSerializer.Deserialize<List<JCDStation>>(items);
            return retrieveClosestStation(position, stations.Where(station => station.totalStands.availabilities.stands != 0).ToList());
        }

        private List<JCDContract> contractsContainingCities(string originCity, string destinationCity)
        {
            JCDecauxItem JCDecauxItems = proxy.getContractsList();
            List<JCDContract> JCDContracts = JsonSerializer.Deserialize<List<JCDContract>>(JCDecauxItems.response);
            utils.cleanContractsList(JCDContracts);

            return JCDContracts.Where(contract => contract.name.Contains(originCity) && contract.name.Contains(destinationCity)).ToList();
        }
    }
}
