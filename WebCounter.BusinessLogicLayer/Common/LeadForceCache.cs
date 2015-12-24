using System;
using System.Collections.Generic;
using System.Web;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class LeadForceCache
    {
        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        public static System.Web.Caching.Cache Cache
        {
            get
            {
                return HttpRuntime.Cache;
            }
        }



        /// <summary>
        /// Caches the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        public static void CacheData(string key, object data)
        {
            CacheData(key, data, 120);
        }



        /// <summary>
        /// Caches the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        public static void CacheData(string key, object data, int cacheDuration)
        {
            if (null != data)
            {
                Cache.Insert(key, data, null, DateTime.Now.AddSeconds(cacheDuration), TimeSpan.Zero);
            }
        }



        /// <summary>
        /// Clears the cache items.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        public static void ClearCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            var itemsToRemove = new List<string>();

            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                {
                    itemsToRemove.Add(enumerator.Key.ToString());
                }
            }

            foreach (var itemToRemove in itemsToRemove)
            {
                Cache.Remove(itemToRemove);
            }
        }
    }
}
