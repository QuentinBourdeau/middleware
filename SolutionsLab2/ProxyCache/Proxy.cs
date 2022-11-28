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
    class Proxy : IProxy
    {
        string apiKey= "apiKey=41a669509b4e45db31dd29c98b811fde4c7b0ae0" ;

        private static HttpClient client = new HttpClient();
        public string Request(string url)
        {
            // 1. Check if the response is in the cache.
            ObjectCache cache = MemoryCache.Default;
            string response = cache[url + "?" + apiKey] as string;

            // 2. If not, call the routing server.
            if (response == null)
            {
                BasicHttpBinding binding = new BasicHttpBinding();

                binding.MaxReceivedMessageSize = 1000000;
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

    }
}
