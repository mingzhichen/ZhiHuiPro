using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using JieYiGuang.Common.Utils;
using System.Collections;

namespace JieYiGuang.Common.Expand.CacheStrategy
{
    public class Provider_Microsoft : ICacheProvider
    {
        private static CacheHelper client;

        static Provider_Microsoft()
        {
            try
            {
                client = new JieYiGuang.Common.Utils.CacheHelper();
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
                client.Insert(key, value);
            }
        }

        public void Add(string key, object value, int cacheSecond)
        {
            if (client != null)
            {
                client.Insert(key, value, cacheSecond);
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
                log4net.LogManager.GetLogger("MicrosoftCacheLog").Info("Get<T>(string key):", ex);
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
                log4net.LogManager.GetLogger("MicrosoftCacheLog").Info("GetList<T>(string key):", ex);
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
                log4net.LogManager.GetLogger("MicrosoftCacheLog").Info("GetIList<T>(string key):", ex);
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
                log4net.LogManager.GetLogger("MicrosoftCacheLog").Info("GetIQueryable<T>(string key):", ex);
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
            return client.GetCacheKey();
        }
    }
}
