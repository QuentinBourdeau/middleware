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
        private static readonly ProxyCacheClient proxy;

        static ClientJCDecauxAPI()
        {
            proxy = new ProxyCacheClient();
        }

        public static JCDStation retrieveClosestStation(GeoCoordinate chosenStationGeo, List<JCDStation> stations)
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

        public static JCDStation retrieveClosestStationDeparture(GeoCoordinate position)
        {
            string items = proxy.getStationsList().response;
            List<JCDStation> stations = JsonSerializer.Deserialize<List<JCDStation>>(items);
            return retrieveClosestStation(position, stations.Where(station => station.totalStands.availabilities.bikes != 0).ToList());
        }

        public static JCDStation retrieveClosestStationArrival(GeoCoordinate position)
        {
            string items = proxy.getStationsList().response;
            List<JCDStation> stations = JsonSerializer.Deserialize<List<JCDStation>>(items);
            return retrieveClosestStation(position, stations.Where(station => station.totalStands.availabilities.stands != 0).ToList());
        }
    }
}
