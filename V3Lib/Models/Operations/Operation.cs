using MessagePack;
using MongoDB.Bson.Serialization.Attributes;
using V3Lib.BsonExtensions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Operations
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(MoreOperation))]
    // Json
    [AddJsonTypeName]
    public abstract class Operation { }
}