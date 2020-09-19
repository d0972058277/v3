using MongoDB.Driver;

namespace V3Lib.Strategies.Abstractions
{
    public interface IMongoComponentStrategy : IComponentStrategy
    {
        IMongoClient MongoClient { get; }
        string Database { get; }
        string Collection { get; }
    }
}