using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models.Conditions;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class RedisConfigConditStrategy : IConfigConditStrategy, IRedisStrategy
    {
        public RedisConfigConditStrategy(IDistributedCache cache, string cacheKey)
        {
            Cache = cache;
            CacheKey = cacheKey;
            _configConditsStrategy = new RedisConfigConditsStrategy(cache, cacheKey);
        }

        public string Key { get; set; }

        public IDistributedCache Cache { get; }

        public string CacheKey { get; }

        protected IConfigConditsStrategy _configConditsStrategy;

        public async Task<ConfigCondition> GetAsync()
        {
            var condits = await _configConditsStrategy.GetAsync();
            return condits.Where(c => c.Key == Key).Single();
        }

        public async Task RemoveAsync()
        {
            var condits = await _configConditsStrategy.GetAsync();
            var condit = condits.Where(c => c.Key == Key).Single();
            condits.Remove(condit);
            await _configConditsStrategy.SetAsync(condits);
        }

        public async Task SetAsync(ConfigCondition entity)
        {
            var condits = await _configConditsStrategy.GetAsync();
            var condit = condits.Where(c => c.Key == entity.Key).SingleOrDefault();
            if (condit is null)
            {
                condits.Add(entity);
            }
            else
            {
                condit.Defined = entity.Defined;
            }
            await _configConditsStrategy.SetAsync(condits);
        }
    }

    public class RedisConfigConditsStrategy : IConfigConditsStrategy, IRedisStrategy
    {
        public RedisConfigConditsStrategy(IDistributedCache cache, string cacheKey)
        {
            Cache = cache;
            CacheKey = cacheKey;
        }

        public IDistributedCache Cache { get; }

        public string CacheKey { get; }

        public Task<List<ConfigCondition>> GetAsync()
        {
            return Cache.GetObjectAsync<List<ConfigCondition>>(CacheKey);
        }

        public Task RemoveAsync()
        {
            return Cache.RemoveAsync(CacheKey);
        }

        public Task SetAsync(List<ConfigCondition> entity)
        {
            return Cache.SetObjectAsync<List<ConfigCondition>>(CacheKey, entity);
        }
    }
}