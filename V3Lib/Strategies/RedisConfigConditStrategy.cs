using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class RedisConfigConditStrategy : IConfigConditStrategy<RedisStrategyParams>, IRedisStrategy
    {
        public RedisConfigConditStrategy(IDistributedCache cache, RedisConfigConditsStrategy configConditsStrategy)
        {
            Cache = cache;
            _configConditsStrategy = configConditsStrategy;
        }

        public IDistributedCache Cache { get; }

        protected RedisConfigConditsStrategy _configConditsStrategy;

        public async Task<ConfigCondition> GetAsync(RedisStrategyParams strategyParams)
        {
            var condits = await _configConditsStrategy.GetAsync(strategyParams);
            return condits.Where(c => c.Key == strategyParams.TargetKey).Single();
        }

        public async Task SetAsync(RedisStrategyParams strategyParams, ConfigCondition entity)
        {
            var condits = await _configConditsStrategy.GetAsync(strategyParams);
            var condit = condits.Where(c => c.Key == entity.Key).SingleOrDefault();
            if (condit is null)
            {
                condits.Add(entity);
            }
            else
            {
                condit.Defined = entity.Defined;
            }
            await _configConditsStrategy.SetAsync(strategyParams, condits);
        }

        public async Task RemoveAsync(RedisStrategyParams strategyParams)
        {
            var condits = await _configConditsStrategy.GetAsync(strategyParams);
            var condit = condits.Where(c => c.Key == strategyParams.TargetKey).Single();
            condits.Remove(condit);
            await _configConditsStrategy.SetAsync(strategyParams, condits);
        }
    }

    public class RedisConfigConditsStrategy : IConfigConditsStrategy<RedisStrategyParams>, IRedisStrategy
    {
        public RedisConfigConditsStrategy(IDistributedCache cache)
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