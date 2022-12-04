using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
    internal class GenericCache<T> where T : System.IComparable<T>, new()
    {
        private static MemoryCache cache = MemoryCache.Default;
        public DateTimeOffset dt_default = ObjectCache.InfiniteAbsoluteExpiration;
        public T Get(string CacheItemName)
        {
            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is "dt_default" 
             * ( public DateTimeOffset dt_default in ProxyCache class).
             * At the instanciation of a ProxyCache object,
             * dt_default = ObjectCache.InfiniteAbsoluteExpiration (no expiration time),
             * but dt_default can be changed. */
            if (CacheItemName == null)
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
            }
        }

        public T Get(string CacheItemName, double dt_seconds)
        {

            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is now + dt_seconds seconds. */
            if (CacheItemName == null)
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
            }



        }

        public T Get(string CacheItemName, DateTimeOffset dt)
        {

            /*where CacheItemName is the key of the entry in the cache. I
             * If CacheItemName doesn't exist or has a null content then create a new T
             * object and put it in the cache with CacheItemName as the corresponding key.
             * In this case, the Expiration Time is dt(DateTimeOffset class). */
            if (CacheItemName == null)
            {
                T t = new T();
                cache.Add(CacheItemName, t, dt);
                return t;
            }
            else
            {
                T t = (T)cache[CacheItemName];
                if (t == null)
                {
                    t = new T();
                    cache.Add(CacheItemName, t, dt);
                }
                return t;
            }
        }
    }
}
