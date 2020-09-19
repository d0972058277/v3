using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models.Components;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class RedisComponentStrategy : IComponentStrategy, IRedisStrategy
    {
        public RedisComponentStrategy(IDistributedCache cache, string cacheKey)
        {
            Cache = cache;
            CacheKey = cacheKey;
        }

        public IDistributedCache Cache { get; }

        public string CacheKey { get; }

        public Task<Component> GetAsync()
        {
            return Cache.GetObjectAsync<Component>(CacheKey);
        }

        public Task RemoveAsync()
        {
            return Cache.RemoveAsync(CacheKey);
        }

        public Task SetAsync(Component entity)
        {
            return Cache.SetObjectAsync<Component>(CacheKey, entity);
        }
    }
}