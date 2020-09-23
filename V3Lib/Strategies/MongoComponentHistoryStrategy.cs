using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Models.Components;
using V3Lib.Models.Histories;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class MongoComponentHistoryStrategy : IComponentHistoryStrategy<MongoStrategyParams>, IMongoStrategy
    {
        public MongoComponentHistoryStrategy(IMongoClient mongoClient)
        {
            MongoClient = mongoClient;
        }

        public IMongoClient MongoClient { get; }

        public async Task DischargeAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<ComponentHistory>.Filter;
            var filter = builder.Empty;
            var sort = Builders<ComponentHistory>.Sort.Ascending(f => f.Id);
            var history = await MongoClient
                .GetDatabase(database)
                .GetCollection<ComponentHistory>(collection)
                .FindOneAndDeleteAsync(filter,
                    new FindOneAndDeleteOptions<ComponentHistory, ComponentHistory>
                    {
                        Sort = sort
                    }
                );
        }

        public Task<List<ComponentHistory>> GetAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<ComponentHistory>.Filter;
            var filter = builder.Empty;
            return MongoClient
                .GetDatabase(database)
                .GetCollection<ComponentHistory>(collection)
                .Find(filter)
                .ToListAsync();
        }

        public Task<ComponentHistory> PeekAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<ComponentHistory>.Filter;
            var filter = builder.Empty;
            return MongoClient
                .GetDatabase(database)
                .GetCollection<ComponentHistory>(collection)
                .Find(filter)
                .SortByDescending(c => c.Id)
                .Limit(1)
                .SingleAsync();
        }

        public Task<ComponentHistory> PopAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<ComponentHistory>.Filter;
            var filter = builder.Empty;
            var sort = Builders<ComponentHistory>.Sort.Descending(f => f.Id);
            return MongoClient
                .GetDatabase(database)
                .GetCollection<ComponentHistory>(collection)
                .FindOneAndDeleteAsync(filter,
                    new FindOneAndDeleteOptions<ComponentHistory, ComponentHistory>
                    {
                        Sort = sort
                    }
                );
        }

        public Task PushAsync(MongoStrategyParams strategyParams, ComponentHistory history)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            return MongoClient.GetDatabase(database).GetCollection<ComponentHistory>(collection).InsertOneAsync(history);
        }

        public Task RemoveAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<ComponentHistory>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(database).GetCollection<ComponentHistory>(collection).DeleteManyAsync(filter);
        }

        public async Task SetAsync(MongoStrategyParams strategyParams, List<ComponentHistory> entity)
        {
            await RemoveAsync(strategyParams);
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            await MongoClient.GetDatabase(database).GetCollection<ComponentHistory>(collection).InsertManyAsync(entity);
        }
    }
}