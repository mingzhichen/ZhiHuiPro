using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NorthScale.Store;
using Enyim.Caching.Memcached;

namespace JieYiGuang.Common.Expand.CacheStrategy
{
    /// <summary>
    /// Memcached 分布式缓存
    /// </summary>
    public class Provider_Memcached : ICacheProvider
    {
        private static NorthScaleClient client;
        static Provider_Memcached()
        {
            try
            {
                client = new NorthScaleClient();
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("MemcachedCacheLog").Info("EnyimMemcachedProvider", ex);
            }
        }

        #region Add
        public void Add(string key, object value)
        {
            if (client != null)
            {
                client.Store(StoreMode.Set, key, value);
            }
        }

        public void Add(string key, object value, int cacheSecond)
        {
            if (client != null)
            {
                client.Store(StoreMode.Set, key, value, new TimeSpan(0, 0, cacheSecond));
            }
        }
        #endregion

        #region Get

        public object Get(string key)
        {
            if (client == null) { return null; } return client.Get(key);
        }

        public T Get<T>(string key)
        {
            try
            {
                return (T)client.Get(key);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("MemcachedCacheLog").Info("Get<T>(string key):", ex);
                this.Remove(key);
                return default(T);
            }
        }

        public List<T> GetList<T>(string key)
        {
            try
            {
                return (List<T>)client.Get(key);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("MemcachedCacheLog").Info("GetList<T>(string key):", ex);
                this.Remove(key);
                return default(List<T>);
            }
        }

        public IList<T> GetIList<T>(string key)
        {
            try
            {
                return (IList<T>)client.Get(key);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("MemcachedCacheLog").Info("GetIList<T>(string key):", ex);
                this.Remove(key);
                return default(IList<T>);
            }
        }


        public IQueryable<T> GetIQueryable<T>(string key)
        {
            try
            {
                return (IQueryable<T>)client.Get(key);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("MemcachedCacheLog").Info("GetIQueryable<T>(string key):", ex);
                this.Remove(key);
                return default(IQueryable<T>);
            }
        }

        #endregion

        #region Remove
        public void Remove(string key)
        {

            if (client != null) { client.Remove(key); }
        }
        #endregion


        public List<String> GetCacheKey()
        {
            JieYiGuang.Common.Utils.LogHelper.LogHandler.LogError("未加载缓存器");
            return null;
        }
    }


}
