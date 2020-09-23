using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class MongoConfigConditStrategy : IConfigConditsStrategy<MongoStrategyParams>, IMongoStrategy
    {
        public MongoConfigConditStrategy(IMongoClient mongoClient)
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