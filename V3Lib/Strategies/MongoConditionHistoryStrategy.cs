using MongoDB.Driver;
using V3Lib.Models.Histories;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class MongoConditionHistoryStrategy : MongoHistoryStrategy<ConditionHistory>
    {
        public MongoConditionHistoryStrategy(IMongoClient mongoClient) : base(mongoClient) { }
    }
}