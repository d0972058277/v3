using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using V3Lib.Models.Components;

namespace V3Lib.Strategies.Abstractions
{
    public interface IStrategy { }

    public interface IMongoStrategy : IStrategy
    {
        IMongoClient MongoClient { get; }
        string Database { get; }
        string Collection { get; }
    }

    public interface IRedisStrategy : IStrategy
    {
        IDistributedCache Cache { get; }
        string CacheKey { get; }
    }

    public interface IStrategy<T> : IStrategy
    {
        Task<T> GetAsync();
        Task SetAsync(T entity);
        Task RemoveAsync();
    }
}