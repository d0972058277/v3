using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class MongoConfigConditStrategy : IConfigConditStrategy<MongoStrategyParams>, IMongoStrategy
    {
        public MongoConfigConditStrategy(IMongoClient mongoClient)
        {
            MongoClient = mongoClient;
        }

        public IMongoClient MongoClient { get; }

        public Task<ConfigCondition> GetAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var key = strategyParams.TargetKey;
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Eq(c => c.Key, key);
            return MongoClient.GetDatabase(database).GetCollection<ConfigCondition>(collection).Find(filter).SingleAsync();
        }

        public Task RemoveAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var key = strategyParams.TargetKey;
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Eq(c => c.Key, key);
            return MongoClient.GetDatabase(database).GetCollection<ConfigCondition>(collection).DeleteOneAsync(filter);
        }

        public async Task SetAsync(MongoStrategyParams strategyParams, ConfigCondition entity)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            await RemoveAsync(strategyParams);
            await MongoClient.GetDatabase(database).GetCollection<ConfigCondition>(collection).InsertOneAsync(entity);
        }
    }

    public class MongoConfigConditsStrategy : IConfigConditsStrategy<MongoStrategyParams>, IMongoStrategy
    {
        public MongoConfigConditsStrategy(IMongoClient mongoClient)
        {
            MongoClient = mongoClient;
        }

        public IMongoClient MongoClient { get; }

        public Task<List<ConfigCondition>> GetAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(database).GetCollection<ConfigCondition>(collection).Find(filter).ToListAsync();
        }

        public Task RemoveAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(database).GetCollection<ConfigCondition>(collection).DeleteManyAsync(filter);
        }

        public async Task SetAsync(MongoStrategyParams strategyParams, List<ConfigCondition> entity)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            await RemoveAsync(strategyParams);
            await MongoClient.GetDatabase(database).GetCollection<ConfigCondition>(collection).InsertManyAsync(entity);
        }
    }
}