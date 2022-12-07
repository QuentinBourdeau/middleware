using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.ServiceModel;
using System.Device.Location;
using System.Collections;
using System.Globalization;

namespace test
{
    internal class ApiOpenRoute
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string API_KEY = "5b3ce3597851110001cf6248533c8f297d74424baa814af18ec650eb";

        public async Task<GeoCoordinate> addressToPoint(string address)
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
            Location ret = JsonSerializer.Deserialize<Location[]>(responseData).First();
            return ret.GetGeoCoordinate();
        }

        public async Task<Rootobject> geoToItinerary(GeoCoordinate startingPosition, GeoCoordinate endPosition, Boolean bicycle)
        {
            // exemple https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248b3387dbe4cbc4881a29750ec80c1b64a&start=8.681495,49.41461&end=8.687872,49.420318
            string url = "https://api.openrouteservice.org/v2/directions/" + (bicycle ? "cycling-regular" : "foot-walking") + "?api_key=";
            string text = "&start=" + startingPosition.Longitude.ToString(CultureInfo.InvariantCulture) + "," + startingPosition.Latitude.ToString(CultureInfo.InvariantCulture) + "&end=" +
                endPosition.Longitude.ToString(CultureInfo.InvariantCulture) + "," + endPosition.Latitude.ToString(CultureInfo.InvariantCulture) + "&size=1";


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
