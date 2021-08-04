using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.Framework
{
    public interface ICacheService
    {
        T TryGetValue<T>(string cacheKey) where T : class;

        void Set<T>(string cacheKey, T cacheValue) where T : class;
    }

    //Will need to move to infrastructure code for wider and more generic usage and to be unit tested in isolation as well
    //This will allow to mock and test the DateTime dependency injected here - Which makes sense for Caching (when moved to a field)
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDateTimeProvider dateTimeProvider;

        public CacheService(IMemoryCache memoryCache, IDateTimeProvider dateTimeProvider)
        {
            this.memoryCache = memoryCache;
            this.dateTimeProvider = dateTimeProvider;
        }

        public T TryGetValue<T>(string cacheKey) where T : class
        {
            if (memoryCache.TryGetValue(cacheKey, out T cacheValue))
                return cacheValue;

            return (T)null;
        }

        public void Set<T>(string cacheKey, T cacheValue) where T : class
        {
            memoryCache.Set(cacheKey, cacheValue, GetMemoryCacheEntryOptions());
        }
        
        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions()
        {
            return new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(dateTimeProvider.GetDateTimeUtcNow().AddHours(24))
                        //.SetSlidingExpiration(new TimeSpan(0, 15, 0)) // Removing for test/assessment of caching efficiency on Azure Web App
                        .SetPriority(CacheItemPriority.Normal);
        }
    }
}
