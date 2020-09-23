using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Creationals;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;
using V3Lib.Visitors;

namespace V3Lib.Strategies
{
    public class MongoComponentStrategy : IComponentStrategy<MongoStrategyParams>, IMongoStrategy
    {
        public MongoComponentStrategy(IMongoClient mongoClient, VisitorFactory visitorFactory)
        {
            MongoClient = mongoClient;
            _visitorFactory = visitorFactory;
        }

        public IMongoClient MongoClient { get; }

        protected VisitorFactory _visitorFactory;

        public async Task<Component> GetAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<Component>.Filter;
            var filter = builder.Empty;
            var components = await MongoClient.GetDatabase(database).GetCollection<Component>(collection).Find(filter).ToListAsync();
            var component = components.GetTree();
            return component;
        }

        public async Task SetAsync(MongoStrategyParams strategyParams, Component entity)
        {
            await RemoveAsync(strategyParams);

            var flatVisitor = _visitorFactory.GetVisitor<FlatComponentVisitor>();
            entity.Accept(flatVisitor);
            var flatComponents = flatVisitor.FlatElements.Values.ToList();
            flatComponents.ForEach(flatComponent => flatComponent.ClearComposite());

            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            await MongoClient.GetDatabase(database).GetCollection<Component>(collection).InsertManyAsync(flatComponents);
        }

        public Task RemoveAsync(MongoStrategyParams strategyParams)
        {
            var database = strategyParams.Database;
            var collection = strategyParams.Collection;
            var builder = Builders<Component>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(database).GetCollection<Component>(collection).DeleteManyAsync(filter);
        }
    }
}