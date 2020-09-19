using MessagePack;
using MongoDB.Bson.Serialization.Attributes;
using V3Lib.BsonExtensions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Additionals
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(MemberAdditional))]
    // Json
    [AddJsonTypeName]
    public abstract class Additional { }
}