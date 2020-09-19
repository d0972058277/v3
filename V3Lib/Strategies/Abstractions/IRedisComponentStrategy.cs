using Microsoft.Extensions.Caching.Distributed;

namespace V3Lib.Strategies.Abstractions
{
    public interface IRedisComponentStrategy : IComponentStrategy
    {
        IDistributedCache Cache { get; }
        string Key { get; }
    }
}