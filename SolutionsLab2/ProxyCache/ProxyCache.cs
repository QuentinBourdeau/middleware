using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Runtime.Caching;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ProxyCache
{
    class ProxyCache : IProxyCache
    {
        private static HttpClient client = new HttpClient();
        private static MemoryCache cache = MemoryCache.Default;
        //private static ObjectCache cache2 = MemoryCache.Default;
        //private static CacheItemPolicy policy = new CacheItemPolicy();
        public DateTimeOffset dt_default = ObjectCache.InfiniteAbsoluteExpiration;
        
        string apiKey= "apiKey=41a669509b4e45db31dd29c98b811fde4c7b0ae0" ;
        
        private T Get<T>(string CacheItemName)
        {
            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is "dt_default" 
             * ( public DateTimeOffset dt_default in ProxyCache class).
             * At the instanciation of a ProxyCache object,
             * dt_default = ObjectCache.InfiniteAbsoluteExpiration (no expiration time),
             * but dt_default can be changed. */
            return Get<T>(CacheItemName, dt_default);
        }

        private T Get<T>(string CacheItemName, double dt_seconds)
        {

            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is now + dt_seconds seconds. */
            return Get<T>(CacheItemName, DateTimeOffset.Now.AddSeconds(dt_seconds));


            
        }

        private T Get<T>(string CacheItemName, DateTimeOffset dt)
        {

            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is dt(DateTimeOffset class). */
            T t = (T)cache[CacheItemName];
            if (!(cache.Contains(CacheItemName)) || cache[CacheItemName] == null)
            {
                cache.Set(CacheItemName, t, dt);
            }
            return t;

            /* Version Dubois
            T response = (T) cache[cacheItemName];
            if (response == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = dt;
                response = JsonSerializer.Deserialize<T>(JCDecauxAPIGetCall(cacheItemName).Result);
                cache.Set(cacheItemName, response, policy);
            }
            return response;*/
        }


        /*Design a JCDecauxItem class with a constructor which makes a request to the
         * JCDecaux API to create a JCDecauxItem object. The structure of this class 
         * depends on the targetted API's endpoint (and so on the retrieved data).)*/

        /* Généré par copilot
         * public JCDecauxItem(string contractName, string stationNumber)
        {
            {
                string url = "https://api.jcdecaux.com/vls/v3/stations/" + stationNumber;
                string query = "contract=" + contractName + "&" + apiKey;
                string response = JCDecauxAPICall(url, query).Result;
                JObject jObject = JObject.Parse(response);
                name = (string)jObject["name"];
                address = (string)jObject["address"];
                position = (string)jObject["position"];
                banking = (string)jObject["banking"];
                bonus = (string)jObject["bonus"];
                status = (string)jObject["status"];
                contract_name = (string)jObject["contract_name"];
                bike_stands = (string)jObject["bike_stands"];
                available_bike_stands = (string)jObject["available_bike_stands"];
                available_bikes = (string)jObject["available_bikes"];
                last_update = (string)jObject["last_update"];
            }*/

        private class JCDecauxItem
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public string Address { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public int BikeStands { get; set; }
            public int AvailableBikes { get; set; }

            public JCDecauxItem(int itemNumber)
            {
                
                var url = $"https://api.jcdecaux.com/vls/v3/stations/{itemNumber}";

                using (var httpClient = new HttpClient())
                {
                    // Make a GET request to the API endpoint.
                    var response = httpClient.GetAsync(url).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // If the request is successful, parse the
                        // response JSON data and populate the
                        // object properties with the retrieved values.
                        var jsonData = response.Content.ReadAsStringAsync().Result;
                        dynamic parsedData = JsonSerializer.Deserialize<Task>(jsonData);
                        Name = parsedData.name;
                        Number = parsedData.number;
                        Address = parsedData.address;
                        Latitude = parsedData.position.lat;
                        Longitude = parsedData.position.lng;
                        BikeStands = parsedData.bike_stands;
                        AvailableBikes = parsedData.available_bikes;
                    }
                }
            }
        }


        public string Request(string url)
        {
            // 1. Check if the response is in the cache.
            //ObjectCache cache = MemoryCache.Default;
            string response = cache[url + "?" + apiKey] as string;

            // 2. If not, call the routing server.
            if (response == null)
            {
                BasicHttpBinding binding = new BasicHttpBinding();

                response = JCDecauxAPICall(url, apiKey).Result;
                // 3. Add the response to the cache.
                cache.Add(url + "?" + apiKey, response, DateTimeOffset.Now.AddSeconds(10));
            }
            // 4. Display the response.
            return (response);
        }

        public string getContractsList()
        {
            string url = "https://api.jcdecaux.com/vls/v3/contracts";
            return JCDecauxAPICall(url, apiKey).Result;
        }

        public string getStationsList()
        {
            BasicHttpBinding binding = new BasicHttpBinding();

            string url = "https://api.jcdecaux.com/vls/v3/stations";
            return JCDecauxAPICall(url, apiKey).Result;
        }

        public string getStationsListWithContractName(string contractName)
        {
            string url = "https://api.jcdecaux.com/vls/v3/stations";
            string query = "contract=" + contractName + "&" + apiKey;
            return JCDecauxAPICall(url, query).Result;
        }

        static async Task<string> JCDecauxAPICall(string url, string query)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + "?" + query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /*Use this JCDecauxItem class in the GenericProxyCache you created to manage requests to JCDecaux on the fly.
                */

    }
}
