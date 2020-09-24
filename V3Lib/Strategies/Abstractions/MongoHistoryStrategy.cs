using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Models.Histories;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public abstract class MongoHistoryStrategy<Entity> : IHistoryStrategy<MongoStrategyParams, Entity>, IMongoStrategy where Entity : History
    {
        protected MongoHistoryStrategy(IMongoClient mongoClient)
        {
            MongoClient = mongoClient;
        }

        public IMongoClient MongoClient { get; }

        public async Task DischargeAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<Entity>.Filter;
            var filter = builder.Empty;
            var sort = Builders<Entity>.Sort.Ascending(f => f.Id);
            var history = await MongoClient
                .GetDatabase(database)
                .GetCollection<Entity>(collection)
                .FindOneAndDeleteAsync(filter,
                    new FindOneAndDeleteOptions<Entity, Entity>
                    {
                        Sort = sort
                    }
                );
        }

        public Task<List<Entity>> GetAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<Entity>.Filter;
            var filter = builder.Empty;
            return MongoClient
                .GetDatabase(database)
                .GetCollection<Entity>(collection)
                .Find(filter)
                .ToListAsync();
        }

        public Task<Entity> PeekAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<Entity>.Filter;
            var filter = builder.Empty;
            return MongoClient
                .GetDatabase(database)
                .GetCollection<Entity>(collection)
                .Find(filter)
                .SortByDescending(c => c.Id)
                .Limit(1)
                .SingleAsync();
        }

        public Task<Entity> PopAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<Entity>.Filter;
            var filter = builder.Empty;
            var sort = Builders<Entity>.Sort.Descending(f => f.Id);
            return MongoClient
                .GetDatabase(database)
                .GetCollection<Entity>(collection)
                .FindOneAndDeleteAsync(filter,
                    new FindOneAndDeleteOptions<Entity, Entity>
                    {
                        Sort = sort
                    }
                );
        }

        public async Task PushAsync(MongoStrategyParams strategyParams, Entity history)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var filter = Builders<Entity>.Filter.Empty;

            while ((await MongoClient.GetDatabase(database).GetCollection<Entity>(collection).CountDocumentsAsync(filter)) >= strategyParams.StackSize)
            {
                await DischargeAsync(strategyParams);
            }

            await MongoClient.GetDatabase(database).GetCollection<Entity>(collection).InsertOneAsync(history);
        }

        public Task RemoveAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<Entity>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(database).GetCollection<Entity>(collection).DeleteManyAsync(filter);
        }

        public async Task SetAsync(MongoStrategyParams strategyParams, List<Entity> entities)
        {
            await RemoveAsync(strategyParams);
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            await MongoClient.GetDatabase(database).GetCollection<Entity>(collection).InsertManyAsync(entities);
        }
    }
}