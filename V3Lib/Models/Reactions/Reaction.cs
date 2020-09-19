using MessagePack;
using MongoDB.Bson.Serialization.Attributes;
using V3Lib.BsonExtensions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Reactions
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(AppPageReaction))]
    [Union(1, typeof(InternalBrowserReaction))]
    [Union(2, typeof(ExternalBrowserReaction))]
    // Json
    [AddJsonTypeName]
    public abstract class Reaction
    {
        public string Path { get; set; }
    }
}