using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.ServiceModel;

namespace test
{
    internal class ApiOpenRoute
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string API_KEY = "5b3ce3597851110001cf6248533c8f297d74424baa814af18ec650eb";

        public async Task<Location[]> addressToPoint(string address)
        {
            string url = "https://nominatim.openstreetmap.org/?";
            string text = "&addressdetails=1" + "&q=" + address + "&format=json" + "&limit=1";

            var baseAddress = new Uri(url + text);

            /*using (var httpClient = new HttpClient {})
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

                using (var response = await httpClient.GetAsync(baseAddress))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseData);
                    return JsonSerializer.Deserialize<Geopoints>(responseData);
                }
            }*/
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "*/*");
            client.DefaultRequestHeaders.TryAddWithoutValidation("accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.TryAddWithoutValidation("connection", "keep-alive");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "PostmanRuntime/7.29.0");
            var response = await client.GetAsync(baseAddress);
            string responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Location[]>(responseData);
        }
        public async Task<Rootobject> addressesToItinerary(string start, string end, Boolean bicycle)
        {
            Task<Location[]> startingPosition = addressToPoint(start);
            Task<Location[]> endPosition = addressToPoint(end);
            startingPosition.Wait();
            endPosition.Wait();
            // exemple https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248b3387dbe4cbc4881a29750ec80c1b64a&start=8.681495,49.41461&end=8.687872,49.420318
            string url = "https://api.openrouteservice.org/v2/directions/" + (bicycle ? "cycling-regular" : "foot-walking") + "?api_key=";
            string text = "&start=" + startingPosition.Result[0].lon + "," + startingPosition.Result[0].lat + "&end=" +
                endPosition.Result[0].lon + "," + endPosition.Result[0].lat + "&size=1";


            var baseAddress = new Uri(url + API_KEY + text);

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

                using (var response = await httpClient.GetAsync(""))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Rootobject>(responseData);

                }
            }
        }
        public async Task<Rootobject> addressesToItinerary(Location startingPosition, Location endPosition, Boolean bicycle)
        {
            // exemple https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248b3387dbe4cbc4881a29750ec80c1b64a&start=8.681495,49.41461&end=8.687872,49.420318
            string url = "https://api.openrouteservice.org/v2/directions/" + (bicycle ? "cycling-regular" : "foot-walking") + "?api_key=";
            string text = "&start=" + startingPosition.lon + "," + startingPosition.lat + "&end=" +
                endPosition.lon + "," + endPosition.lat + "&size=1";


            var baseAddress = new Uri(url + API_KEY + text);

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

                using (var response = await httpClient.GetAsync(""))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Rootobject>(responseData);

                }
            }
        }
    }
}
