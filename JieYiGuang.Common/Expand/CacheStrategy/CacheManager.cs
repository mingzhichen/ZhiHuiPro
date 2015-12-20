using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JieYiGuang.Common.Expand.CacheStrategy
{
    /// <summary>
    /// 缓存扩展管理类  Memcached Microsoft
    /// </summary>
    public class CacheManager
    {
        private static ICacheProvider cs;
        private static volatile CacheManager instance = null;
        private static object lockHelper = new object();
        private string CacheInstalKey = "InstalCache";


        public CacheManager()
        {
            string type = ConfigurationManager.AppSettings["CacheStrategy"];
                cs = new Provider_Microsoft();
        }

        public static CacheManager GetCachingService()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new CacheManager();
                    }
                }
            } return instance;
        }

        #region ConfigCache
        /// <summary>
        /// 全局缓存集合
        /// </summary>
        /// <param name="cacheKey"></param>
        public void ConfigCacheInstal(string cacheKey)
        {
            lock (lockHelper)
            {
                var CacheList = cs.GetList<string>(CacheInstalKey);
                if (CacheList == null)
                {
                    CacheList = new List<string>();
                    CacheList.Add(cacheKey);
                }
                else
                {
                    //检查是否已存在缓存Key
                    int count = CacheList.Count(p => p == cacheKey);
                    if (count == 0)
                    {
                        CacheList.Add(cacheKey);
                    }
                }
                cs.Add(CacheInstalKey, CacheList);
            }
        }

        /// <summary>
        /// 全局缓存移除
        /// </summary>
        /// <param name="cacheKey"></param>
        public void ConfigCacheRemovePrefix(string cacheKeyPrefix)
        {
            lock (lockHelper)
            {
                var CacheList = cs.GetList<string>(CacheInstalKey);
                if (CacheList != null)
                {
                    foreach (var item in CacheList)
                    {
                        if (item.IndexOf(cacheKeyPrefix) != -1)
                        {
                            cs.Remove(item);
                        }
                    }
                }
            }
        }

        #endregion

        #region Add

        public void Add(string key, object value)
        {
            if (String.IsNullOrEmpty(key) || value == null) return;
            lock (lockHelper)
            {
                cs.Add(key, value);
            }
        }
        public void Add(string key, object value, int cacheSecond)
        {
            if (String.IsNullOrEmpty(key) || value == null) return;
            lock (lockHelper)
            {
                cs.Add(key, value, cacheSecond);
            }
        }

        #endregion

        #region Get

        public object Get(string key)
        {
            return cs.Get(key);
        }

        public T Get<T>(string key)
        {
            return cs.Get<T>(key);
        }
        public List<T> GetList<T>(string key)
        {
            return cs.GetList<T>(key);
        }
        public IList<T> GetIList<T>(string key)
        {
            return cs.GetIList<T>(key);
        }
        public IQueryable<T> IQueryable<T>(string key)
        {
            return cs.GetIQueryable<T>(key);
        }
        #endregion

        #region Remove
        public void Remove(string key)
        {
            lock (lockHelper) { cs.Remove(key); }
        }
        #endregion

        #region 获取缓存键列表
        public List<String> GetCacheKey()
        {
            return cs.GetCacheKey();
        }
        #endregion
    }
}
