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
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    class Proxy : IProxy
    {
        private static HttpClient client = new HttpClient();
        public string Request(string url, string query)
        {
            // 1. Check if the response is in the cache.
            ObjectCache cache = MemoryCache.Default;
            string response = cache[url + "?" + query] as string;

            // 2. If not, call the routing server.
            if (response == null)
            {
                BasicHttpBinding binding = new BasicHttpBinding();

                binding.MaxReceivedMessageSize = 1000000;
                response = JCDecauxAPICall(url, query).Result;
                // 3. Add the response to the cache.
                cache.Add(url + "?" + query, response, DateTimeOffset.Now.AddSeconds(10));
            }
            // 4. Display the response.
            return(response);
        }

        public string getContractsList(string queryTemp)
        {
            string url = "https://api.jcdecaux.com/vls/v3/contracts";
            string query = queryTemp;
            return JCDecauxAPICall(url, query).Result;
        }

        public string getStationsList(string queryTemp)
        {
            BasicHttpBinding binding = new BasicHttpBinding();

            binding.MaxReceivedMessageSize = 1000000;
            string url = "https://api.jcdecaux.com/vls/v3/stations";
            string query = queryTemp;
            return JCDecauxAPICall(url, query).Result;
        }

        public string getStationsListWithContractName(string contractName, string queryTemp)
        {
            string url = "https://api.jcdecaux.com/vls/v3/stations";
            string query = "contract=" + contractName + "&" + queryTemp;
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
