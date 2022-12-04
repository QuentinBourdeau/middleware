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

namespace GenericProxyCache
{
    class ProxyCache : IProxyCache
    {
        private static HttpClient client = new HttpClient();
        private static MemoryCache cache = MemoryCache.Default;
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
            if (!(cache.Contains(CacheItemName)) || t == null)
            {
                t = (T)Activator.CreateInstance(typeof(T), CacheItemName);
                cache.Set(CacheItemName, t, dt);
            }
            return t;
        }

       
        public JCDecauxItem getContractsList()
        {
            string url = "https://api.jcdecaux.com/vls/v3/contracts?"+apiKey;
            return Get<JCDecauxItem>(url);
            //return JCDecauxAPICall(url, apiKey).Result;
        }

        public JCDecauxItem getStationsList()
        {
            BasicHttpBinding binding = new BasicHttpBinding();

            string url = "https://api.jcdecaux.com/vls/v3/stations?"+apiKey;
            return Get<JCDecauxItem>(url);
            //return JCDecauxAPICall(url, apiKey).Result;
        }

        public JCDecauxItem getStationsListWithContractName(string contractName)
        {
            string url = "https://api.jcdecaux.com/vls/v3/stations";
            string query = "contract=" + contractName + "&" + apiKey;
            return Get<JCDecauxItem>(url+ query);
            //return JCDecauxAPICall(url, query).Result;
        }


    }
}
