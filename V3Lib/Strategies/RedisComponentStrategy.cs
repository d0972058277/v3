using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models.Components;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class RedisComponentStrategy : IRedisComponentStrategy
    {
        public IDistributedCache Cache { get; }

        public string Key { get; }

        public Task<Component> GetAsync()
        {
            return Cache.GetObjectAsync<Component>(Key);
        }

        public Task RemoveAsync()
        {
            return Cache.RemoveAsync(Key);
        }

        public Task SetAsync(Component component)
        {
            return Cache.SetObjectAsync<Component>(Key, component);
        }
    }
}