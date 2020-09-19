using MessagePack;
using MongoDB.Bson.Serialization.Attributes;
using V3Lib.BsonExtensions;
using V3Lib.Filters.Abstractions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Conditions
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(DefinedCondition))]
    [Union(1, typeof(ReferenceCondition))]
    // Json
    [AddJsonTypeName]
    public abstract class Condition
    {
        public abstract bool Pass(IFilter filter);
    }
}