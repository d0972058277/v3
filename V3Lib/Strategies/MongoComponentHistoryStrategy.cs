using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using V3Lib.Models.Components;
using V3Lib.Models.Histories;
using V3Lib.Models.Params;
using V3Lib.Strategies.Abstractions;

namespace V3Lib.Strategies
{
    public class MongoComponentHistoryStrategy : MongoHistoryStrategy<ComponentHistory>
    {
        public MongoComponentHistoryStrategy(IMongoClient mongoClient) : base(mongoClient) { }
    }
}