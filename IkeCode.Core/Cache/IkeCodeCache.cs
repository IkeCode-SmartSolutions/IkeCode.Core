using System;
using System.Runtime.Caching;

namespace IkeCode.Core.Cache
{
    public class IkeCodeCache
    {
        #region Attributes

        private ObjectCache Cache { get { return MemoryCache.Default; } }

        #endregion

        #region Public Methods

        public CacheModel AutoCache(string cacheKey, object data, int cacheTime)
        {
            if (cacheTime > 0)
            {
                var expiration = TimeSpan.FromMinutes(cacheTime);
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now + expiration;

                var cache = new CacheModel();
                cache.Expiration = expiration;
                cache.Key = cacheKey;
                cache.Value = data;

                var cacheItem = new CacheItem(cacheKey, cache);
                return (CacheModel)Cache.AddOrGetExisting(cacheItem, policy).Value;
            }
            else
            {
                return GetCacheModelObject(cacheKey, data, cacheTime);
            }
        }

        public CacheModel AutoCache<T>(string cacheKey, int cacheTime, Func<T> retriever)
        {
            if (cacheTime > 0)
            {
                if (!Exists(cacheKey))
                {
                    var data = (T)retriever();
                    Add(cacheKey, data, cacheTime);
                }

                return Get(cacheKey);
            }
            else
            {
                var data = (T)retriever();
                return GetCacheModelObject(cacheKey, data, cacheTime);
            }
        }

        public CacheModel AutoCache<T1, TR>(string cacheKey, int cacheTime, Func<T1, TR> retriever, T1 param)
        {
            if (cacheTime > 0)
            {
                if (!Exists(cacheKey))
                {
                    var data = (TR)retriever(param);
                    Add(cacheKey, data, cacheTime);
                }

                return Get(cacheKey);
            }
            else
            {
                var data = (TR)retriever(param);
                return GetCacheModelObject(cacheKey, data, cacheTime);
            }
        }

        public CacheModel AutoCache<T1, T2, TR>(string cacheKey, int cacheTime, Func<T1, T2, TR> retriever, T1 param, T2 param2)
        {
            if (cacheTime > 0)
            {
                if (!Exists(cacheKey))
                {
                    var data = (TR)retriever(param, param2);
                    Add(cacheKey, data, cacheTime);
                }
                return Get(cacheKey);
            }
            else
            {
                var data = (TR)retriever(param, param2);
                return GetCacheModelObject(cacheKey, data, cacheTime);
            }
        }

        public CacheModel AutoCache<T1, T2, T3, TR>(string cacheKey, int cacheTime, Func<T1, T2, T3, TR> retriever, T1 param, T2 param2, T3 param3)
        {
            if (cacheTime > 0)
            {
                if (!Exists(cacheKey))
                {
                    var data = (TR)retriever(param, param2, param3);
                    Add(cacheKey, data, cacheTime);
                }

                return Get(cacheKey);
            }
            else
            {
                var data = (TR)retriever(param, param2, param3);
                return GetCacheModelObject(cacheKey, data, cacheTime);
            }
        }

        public CacheModel AutoCache<T1, T2, T3, T4, TR>(string cacheKey, int cacheTime, Func<T1, T2, T3, T4, TR> retriever, T1 param, T2 param2, T3 param3, T4 param4)
        {
            if (cacheTime > 0)
            {
                if (!Exists(cacheKey))
                {
                    var data = (TR)retriever(param, param2, param3, param4);
                    Add(cacheKey, data, cacheTime);
                }

                return Get(cacheKey);
            }
            else
            {
                var data = (TR)retriever(param, param2, param3, param4);
                return GetCacheModelObject(cacheKey, data, cacheTime);
            }
        }

        public CacheModel Get(string cacheKey)
        {
            return (CacheModel)Cache[cacheKey];
        }

        public void Add(string cacheKey, object data, int cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(cacheTime);

            var cache = GetCacheModelObject(cacheKey, data, cacheTime);

            var cacheItem = new CacheItem(cacheKey, cache);
            Cache.Add(cacheItem, policy);
        }

        public bool Exists(string cacheKey)
        {
            return Cache[cacheKey] != null;
        }

        public void Remove(string cacheKey)
        {
            if (Exists(cacheKey))
                Cache.Remove(cacheKey);
        }

        #endregion

        #region Private Methods

        private static CacheModel GetCacheModelObject(string cacheKey, object data, int cacheTime)
        {
            var expiration = TimeSpan.FromMinutes(cacheTime);
            var cache = new CacheModel();
            cache.Expiration = expiration;
            cache.Key = cacheKey;
            cache.Value = data;
            return cache;
        }

        #endregion
    }
}
