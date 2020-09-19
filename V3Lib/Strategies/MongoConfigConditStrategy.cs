using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Models.Conditions;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class MongoConfigConditStrategy : IConfigConditStrategy, IMongoStrategy
    {
        public MongoConfigConditStrategy(IMongoClient mongoClient, string database, string collection)
        {
            MongoClient = mongoClient;
            Database = database;
            Collection = collection;
        }

        public IMongoClient MongoClient { get; }

        public string Database { get; }

        public string Collection { get; }

        public string Key { get; set; }

        public Task<ConfigCondition> GetAsync()
        {
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Eq(c => c.Key, Key);
            return MongoClient.GetDatabase(Database).GetCollection<ConfigCondition>(Collection).Find(filter).SingleAsync();
        }

        public Task RemoveAsync()
        {
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Eq(c => c.Key, Key);
            return MongoClient.GetDatabase(Database).GetCollection<ConfigCondition>(Collection).DeleteOneAsync(filter);
        }

        public async Task SetAsync(ConfigCondition entity)
        {
            await RemoveAsync();
            await MongoClient.GetDatabase(Database).GetCollection<ConfigCondition>(Collection).InsertOneAsync(entity);
        }
    }

    public class MongoConfigConditsStrategy : IConfigConditsStrategy, IMongoStrategy
    {
        public MongoConfigConditsStrategy(IMongoClient mongoClient, string database, string collection)
        {
            MongoClient = mongoClient;
            Database = database;
            Collection = collection;
        }

        public IMongoClient MongoClient { get; }

        public string Database { get; }

        public string Collection { get; }

        public Task<List<ConfigCondition>> GetAsync()
        {
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(Database).GetCollection<ConfigCondition>(Collection).Find(filter).ToListAsync();
        }

        public Task RemoveAsync()
        {
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(Database).GetCollection<ConfigCondition>(Collection).DeleteManyAsync(filter);
        }

        public async Task SetAsync(List<ConfigCondition> entity)
        {
            await RemoveAsync();
            await MongoClient.GetDatabase(Database).GetCollection<ConfigCondition>(Collection).InsertManyAsync(entity);
        }
    }
}