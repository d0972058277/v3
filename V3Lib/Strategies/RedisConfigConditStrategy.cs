using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{

    public class RedisConfigConditStrategy : IConfigConditsStrategy<RedisStrategyParams>, IRedisStrategy
    {
        public RedisConfigConditStrategy(IDistributedCache cache)
        {
            Cache = cache;
        }

        public IDistributedCache Cache { get; }

        public Task<List<ConfigCondition>> GetAsync(RedisStrategyParams strategyParams)
        {
            return Cache.GetObjectAsync<List<ConfigCondition>>(strategyParams.Cachekey);
        }

        public Task RemoveAsync(RedisStrategyParams strategyParams)
        {
            return Cache.RemoveAsync(strategyParams.Cachekey);
        }

        public Task SetAsync(RedisStrategyParams strategyParams, List<ConfigCondition> entity)
        {
            return Cache.SetObjectAsync<List<ConfigCondition>>(strategyParams.Cachekey, entity);
        }
    }
}