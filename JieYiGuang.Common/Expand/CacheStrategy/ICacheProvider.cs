using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace JieYiGuang.Common.Expand.CacheStrategy
{
    /// <summary>
    /// 缓存策略接口
    /// </summary>
    public interface ICacheProvider
    {
        void Add(string key, object value);
        void Add(string key, object value, int cacheSecond);

        object Get(string key);
        T Get<T>(string key);

        List<T> GetList<T>(string key);
        IList<T> GetIList<T>(string key);
        IQueryable<T> GetIQueryable<T>(string key);


        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 获取缓存键值
        /// </summary>
        /// <returns></returns>
        List<String> GetCacheKey();
    }
}
