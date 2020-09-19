using MessagePack;
using MongoDB.Bson.Serialization.Attributes;
using V3Lib.BsonExtensions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Medias
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(ImageMedia))]
    [Union(1, typeof(AudioMedia))]
    [Union(2, typeof(VideoMedia))]
    // Json
    [AddJsonTypeName]
    public abstract class Media
    {
        public string Title { get; set; }
        public string Path { get; set; }
    }
}