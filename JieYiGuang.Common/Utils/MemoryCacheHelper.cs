using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JieYiGuang.Common.Helper
{
    /// <summary>
    /// 基于MemoryCache的缓存辅助类
    /// </summary>

    public class MemoryCache<TK, TV>
    {
        private ObjectCache _memoryCache;
        public static MemoryCache<TK, TV> Instance
        {
            get { return SingletonProvider<MemoryCache<TK, TV>>.Instance; }
        }

        public MemoryCache() : this(null) { }
        public MemoryCache(string name)
        {
            _memoryCache = new MemoryCache(string.Format("{0}-{1}-{2}", typeof(TK).Name, typeof(TV).Name, name));
        }

        public TV Get<TV>(TK cacheKey, Func<TV> getUncachedValue, DateTimeOffset dateTimeOffset)
        {
            if (_memoryCache.Contains(ParseKey(cacheKey)))
            {
                return (TV)_memoryCache[ParseKey(cacheKey)];
            }
            else
            {
                var v = getUncachedValue();
                object o = v;
                _memoryCache.Set(ParseKey(cacheKey), o, dateTimeOffset);
                return v;
            }
        }

        public TV Get<TV>(TK cacheKey, Func<TV> getUncachedValue, TimeSpan timeSpan)
        {
            return Get(cacheKey, getUncachedValue, new DateTimeOffset(DateTime.UtcNow + timeSpan));
        }

        public void Remove(TK cacheKey)
        {
            _memoryCache.Remove(ParseKey(cacheKey));
        }

        private string ParseKey(TK key)
        {
            return key.GetHashCode().ToString();
        }
    }
}
