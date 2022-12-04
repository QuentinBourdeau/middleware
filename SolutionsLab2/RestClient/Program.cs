using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// HttpClient is in the System.web.Http namespace.
using System.Net.Http;
// To use JsonSerializer, you need to add System.Text.Json. Depending on your project type and version, you may find it as assembly reference or Nuget package
// (right-click on the project --> (add --> Reference) or Manage NuGet packages).
using System.Text.Json;
// GeoCordinates is in the System.Device.Location namespace, coming from System.Device which is an assembly reference.
using System.Device.Location;
using RestClient.Proxy;
using System.ServiceModel;

namespace RestClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string query, url, response;

            ApiOpenRoute aor = new ApiOpenRoute();
            Task<Rootobject> iti = aor.addressesToItinerary("Eiffel tower", "Louvres", false);
            Console.WriteLine(iti.Result.features[0].properties.segments[0].steps[0].instruction);
            //Console.WriteLine(List<double>  ld : iti.Result.features[0].geometry.coordinates);

            Proxy.ProxyCacheClient proxy = new Proxy.ProxyCacheClient();
            BasicHttpBinding binding = new BasicHttpBinding();


            response = proxy.getStationsList();
            //List<JCDStation> allStations = JsonSerializer.Deserialize<List<JCDStation>>(response);

            // 2.2: Display all stations.
            Console.WriteLine("Stations:");
            /*foreach (JCDStation item in allStations)
            {
                Console.WriteLine(item.number + ": " + item.name + ", number of bikes available:" + item.mainStands.availabilities.bikes + ", number of available places : " + item.mainStands.availabilities.stands);
            }*/

            // 2.2: Ask the user to choose one.
            Console.WriteLine("Which station are you interested in ?");
            int stationNumber = Int32.Parse(Console.ReadLine().Split(new[] { ':' })[0]);

            // 3.0: Retrieve the chosen station.
            /*JCDStation chosenStation = allStations[0];
            foreach (JCDStation item in allStations)
            {
                if (item.number == stationNumber) {
                    chosenStation = item;
                    break;
                }
            }
            Console.WriteLine("Chosen station: " + chosenStation.name);
            */
            //3.1 : Compute the closest station.
            /*GeoCoordinate stationCoordinates = new GeoCoordinate(chosenStation.position.latitude, chosenStation.position.longitude);

            Double minDistance = -1;
            JCDStation closestStation = chosenStation;
            foreach (JCDStation item in allStations)
            {
                // Find the current station's position.
                GeoCoordinate candidatePos = new GeoCoordinate(item.position.latitude, item.position.longitude);
                // And compare its distance to the chosen one to see if it is closer than the current closest.
                Double distanceToCandidate = stationCoordinates.GetDistanceTo(candidatePos);

                if (distanceToCandidate != 0 && (minDistance == -1 || distanceToCandidate < minDistance))
                {
                    closestStation = item;
                    minDistance = distanceToCandidate;
                }
            }

            Console.WriteLine("Closest station: " + closestStation.name);
            Console.ReadLine();

        }*/

            // Task is the only possible return value on an async function. If you need to specify a type, you can use the <> notation, for instance Task<string>.
            /*static async Task<string> JCDecauxAPICall(string url, string query) 
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url + "?" + query);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }*/
        }

    }
}
