using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Runtime.Caching;
using System.Net.Http;
using System.Threading.Tasks;

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
            return(response);
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


        public T Get<T>(string CacheItemName)
        {
            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is "dt_default" 
             * ( public DateTimeOffset dt_default in ProxyCache class).
             * At the instanciation of a ProxyCache object,
             * dt_default = ObjectCache.InfiniteAbsoluteExpiration (no expiration time),
             * but dt_default can be changed. */
            /*if (!(cache.Contains(CacheItemName)) || cache[CacheItemName] == null)
            {
                T t = new T();
                cache.Add(CacheItemName, t, dt_default);
                return t;
            }
            else
            {
                T t = (T)cache[CacheItemName];
                if (t == null)
                {
                    t = new T();
                    cache.Add(CacheItemName, t, dt_default);
                }
                return t;
            }*/
            return Get<T>(CacheItemName, dt_default);
        }

        public T Get<T>(string CacheItemName, double dt_seconds)
        {

            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is now + dt_seconds seconds. */
            /*if (CacheItemName == null)
            {
                T t = new T();
                cache.Add(CacheItemName, t, DateTimeOffset.Now.AddSeconds(dt_seconds));
                return t;
            }
            else
            {
                T t = (T)cache[CacheItemName];
                if (t == null)
                {
                    t = new T();
                    cache.Add(CacheItemName, t, DateTimeOffset.Now.AddSeconds(dt_seconds));
                }
                return t;
            }*/

            return Get<T>(CacheItemName, DateTimeOffset.Now.AddSeconds(dt_seconds));



        }

        public T Get<T>(string CacheItemName, DateTimeOffset dt)
        {

            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is dt(DateTimeOffset class). */
            T t = (T)cache[CacheItemName];
            if (!(cache.Contains(CacheItemName)) || cache[CacheItemName] == null)
            {
                //T t = new T();
                cache.Add(CacheItemName, t, dt);
                return t;
            }
            else
            {
                cache.Set(CacheItemName, t, dt);
                /*T t = (T)cache[CacheItemName];
                if (t == null)
                {
                    t = new T();
                    cache.Add(CacheItemName, t, dt);
                }
                return t;*/
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

        class JCDecauxItem
        {
            public string name { get; set; }
            public string address { get; set; }
            public string position { get; set; }
            public string banking { get; set; }
            public string bonus { get; set; }
            public string status { get; set; }
            public string contract_name { get; set; }
            public string bike_stands { get; set; }
            public string available_bike_stands { get; set; }
            public string available_bikes { get; set; }
            public string last_update { get; set; }
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


            /*Use this JCDecauxItem class in the GenericProxyCache you created to manage requests to JCDecaux on the fly.
                    */

            /*public class JCDStation
            {
                public int number { get; set; }
                public string name { get; set; }
                public Position position { get; set; }
                public MainStands mainStands { get; set; }
            }

            public class Position
            {
                public Double latitude { get; set; }
                public Double longitude { get; set; }
            }

            public class MainStands
            {
                public Availabilities availabilities { get; set; }
            }

            public class Availabilities
            {
                public int bikes { get; set; }
                public int stands { get; set; }

            }*/

            /*public class JCDContract
    {
        public string name { get; set; }
    }*/
        }
    }
