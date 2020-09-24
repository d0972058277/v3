using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using V3Lib.Models.Components;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public interface IStrategy { }

    public interface IMongoStrategy : IStrategy
    {
        IMongoClient MongoClient { get; }
    }

    public interface IRedisStrategy : IStrategy
    {
        IV3DistributedCache Cache { get; }
    }

    public interface IStrategy<T, E> : IStrategy where T : IStrategyParams
    {
        Task<E> GetAsync(T strategyParams);
        Task SetAsync(T strategyParams, E entity);
        Task RemoveAsync(T strategyParams);
    }
}