using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Text.Json;
using test.ProxyCacheRef;

namespace test
{
    internal class Biking : IBiking
    {
        Utils utils = new Utils();
        ApiOpenRoute openStreet = new ApiOpenRoute();
        ProxyCacheClient genericProxyCache = new ProxyCacheClient();
        bool hasJCDContract = false;
        public Biking()
        {
            //string query, url, response;

            //ApiOpenRoute aor = new ApiOpenRoute();
            //Task<Rootobject> iti = aor.addressesToItinerary("Eiffel tower", "Louvres", false);
            //Console.WriteLine(iti.Result.features[0].properties.segments[0].steps[0].instruction);
            //Console.WriteLine(List<double>  ld : iti.Result.features[0].geometry.coordinates);

            //ProxyCacheClient proxy = new ProxyCacheClient();
            //BasicHttpBinding binding = new BasicHttpBinding();


            //response = proxy.getStationsList();
            //List<JCDStation> allStations = JsonSerializer.Deserialize<List<JCDStation>>(response);

            // 2.2: Display all stations.
            //Console.WriteLine("Stations:");
            /*foreach (JCDStation item in allStations)
            {
                Console.WriteLine(item.number + ": " + item.name + ", number of bikes available:" + item.mainStands.availabilities.bikes + ", number of available places : " + item.mainStands.availabilities.stands);
            }*/

            // 2.2: Ask the user to choose one.
            //Console.WriteLine("Which station are you interested in ?");
            //int stationNumber = Int32.Parse(Console.ReadLine().Split(new[] { ':' })[0]);

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
                        // get closest station using JCDECAUX
            ClientJCDecauxAPI.retrieveClosestStationDeparture(startingPoint);

            List<JCDStation> allstations = JsonSerializer.Deserialize<List<JCDStation>>(JCDecauxItems.response);
            // get both closest station point using nominatim

            //3.1 : Compute the closest station.
            Position stationCoordinates = new Position(chosenStation.position.latitude, chosenStation.position.longitude);

            Double minDistance = -1;
            JCDStation closestStation = chosenStation;
            foreach (JCDStation item in allStations)
            {
                //Find the current station's position.
                Position candidatePos = new Position(item.position.latitude, item.position.longitude);
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
            return null;
        }
            // get all three itinerary
            // concatenate them
            // return the final object

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

        public Itinerary GetItinerary(string origin, string destination)
        {

            //TODO 

            // get starting point and ending point using nominatim
            GeoCoordinate startingPoint = openStreet.addressToPoint(origin).Result;
            GeoCoordinate endingPoint = openStreet.addressToPoint(destination).Result;

            JCDStation startStation = ClientJCDecauxAPI.retrieveClosestStationDeparture(startingPoint);
            JCDStation endingStation = ClientJCDecauxAPI.retrieveClosestStationArrival(startingPoint);

            GeoCoordinate startStationLocation = utils.posToCoor(startStation.position);
            GeoCoordinate endingStationLocation = utils.posToCoor(endingStation.position);

            List<Rootobject> iti = new List<Rootobject>();
            iti.Add(openStreet.geoToItinerary(startingPoint,startStationLocation,false).Result);
            iti.Add(openStreet.geoToItinerary(startStationLocation, endingStationLocation, true).Result);
            iti.Add(openStreet.geoToItinerary(endingStationLocation, endingPoint, false).Result);

            return utils.calculateItinenary(iti);

        }
    }

}
