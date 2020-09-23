using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class RedisTargetConfigConditStrategy : IConfigConditStrategy<RedisStrategyParams>, IRedisStrategy
    {
        public RedisTargetConfigConditStrategy(IDistributedCache cache, RedisConfigConditStrategy configConditsStrategy)
        {
            Cache = cache;
            _configConditsStrategy = configConditsStrategy;
        }

        public IDistributedCache Cache { get; }

        protected RedisConfigConditStrategy _configConditsStrategy;

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
}