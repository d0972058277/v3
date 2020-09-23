using System;
using System.Diagnostics.Contracts;
using MessagePack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using V3Lib.BsonExtensions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Histories
{
    // Bson
    [AddBsonKnowTypes]
    // MsgPack
    [MessagePackObject(true)]
    [Union(0, typeof(ComponentHistory))]
    // Json
    [AddJsonTypeName]
    public abstract class History
    {
        [IgnoreMember]
        [JsonIgnore]
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public HistoryEditor Editor { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public class HistoryEditor
        {
            public string Name { get; set; }
        }
    }
}