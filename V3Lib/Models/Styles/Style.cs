using MessagePack;
using MongoDB.Bson.Serialization.Attributes;
using V3Lib.BsonExtensions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Styles
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(BannerStyle))]
    [Union(1, typeof(RecommendationStyle))]
    // Json
    [AddJsonTypeName]
    public abstract class Style { }
}