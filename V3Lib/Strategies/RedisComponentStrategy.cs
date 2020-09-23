using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class RedisComponentStrategy : IComponentStrategy<RedisStrategyParams>, IRedisStrategy
    {
        public RedisComponentStrategy(IDistributedCache cache)
        {
            Cache = cache;
        }

        public IDistributedCache Cache { get; }

        public Task<Component> GetAsync(RedisStrategyParams strategyParams)
        {
            return Cache.GetObjectAsync<Component>(strategyParams.Cachekey);
        }

        public Task RemoveAsync(RedisStrategyParams strategyParams)
        {
            return Cache.RemoveAsync(strategyParams.Cachekey);
        }

        public Task SetAsync(RedisStrategyParams strategyParams, Component entity)
        {
            return Cache.SetObjectAsync<Component>(strategyParams.Cachekey, entity);
        }
    }
}