using System;
using System.Collections.Generic;
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
            Location startingCity = openStreet.addressToPoint(origin).Result[0];
            Location endingCity = openStreet.addressToPoint(destination).Result[0];
            if (!utils.SameCity(startingCity, endingCity))
            {
                return null;
                //Si les 2 adresses ne sont pas dans la même ville, le programme s'arrête.
            }
            string city = startingCity.address.city;
            genericProxyCache.getStationsList();
            JCDecauxItem JCDecauxItems = genericProxyCache.getContractsList();
            List<JCDContract> JCDContracts = JsonSerializer.Deserialize<List<JCDContract>>(JCDecauxItems.response);
            
            //List<JCDContract> JCDContracts = JsonSerializer.Deserialize<List<JCDContract>>(genericProxyCache.getContractsList());
            //Console.WriteLine(JCDContracts[0]);
            foreach (JCDContract contract in JCDContracts)
            {
                Console.WriteLine(contract.name);
                if (contract.name == city)
                {
                    hasJCDContract = true;
                    break;
                }
            }
            //JCDecauxItem JCDecauxItems = proxy.getStationsList();
            //List<JCDStation> JCDStations = JsonSerializer.Deserialize<List<JCDStation>>(JCDecauxItems.response);

            //TODO 

            // get starting point and ending point using nominatim
            Location startingPoint = openStreet.addressToPoint(origin).Result[0];
            Location endingPoint = openStreet.addressToPoint(destination).Result[0];
            // check if both point have the same city / contract
            if (!utils.SameCity(startingCity, endingCity))
            {
                return null;
            }
            string city = startingPoint.address.city;
            // get closest station using JCDECAUX
            JCDecauxItem JCDecauxItems = genericProxyCache.getContractsList();
            List<JCDContract> JCDContracts = JsonSerializer.Deserialize<List<JCDContract>>(JCDecauxItems.response);
            // get both station point using nominatim
            // get all three itinerary
            // concatenate them
            // return the final object

            return null;
        }
    }

}
