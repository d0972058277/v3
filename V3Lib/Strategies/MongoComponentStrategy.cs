using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Creationals;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Strategies.Abstractions;
using V3Lib.Visitors;

namespace V3Lib.Strategies
{
    public class MongoComponentStrategy : IMongoComponentStrategy
    {
        public MongoComponentStrategy(IMongoClient mongoClient, string database, string collection, VisitorFactory visitorFactory)
        {
            MongoClient = mongoClient;
            Database = database;
            Collection = collection;
            _visitorFactory = visitorFactory;
        }

        public IMongoClient MongoClient { get; }

        public string Database { get; }

        public string Collection { get; }

        protected VisitorFactory _visitorFactory;

        public async Task<Component> GetAsync()
        {
            var builder = Builders<Component>.Filter;
            var filter = builder.Empty;
            var components = await MongoClient.GetDatabase(Database).GetCollection<Component>(Collection).Find(filter).ToListAsync();
            var component = components.GetTree();
            return component;
        }

        public Task RemoveAsync()
        {
            var builder = Builders<Component>.Filter;
            var filter = builder.Empty;
            return MongoClient.GetDatabase(Database).GetCollection<Component>(Collection).DeleteManyAsync(filter);
        }

        public async Task SetAsync(Component component)
        {
            await RemoveAsync();
            var flatVisitor = _visitorFactory.GetVisitor<FlatComponentVisitor>();
            component.Accept(flatVisitor);
            var flatComponents = flatVisitor.FlatElements.Values.ToList();
            flatComponents.ForEach(flatComponent => flatComponent.ClearComposite());
            await MongoClient.GetDatabase(Database).GetCollection<Component>(Collection).InsertManyAsync(flatComponents);
        }
    }
}