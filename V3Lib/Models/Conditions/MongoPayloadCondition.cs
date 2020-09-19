using System;
using MessagePack;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using V3Lib.Filters.Abstractions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class MongoPayloadCondition : Condition
    {
        [IgnoreMember]
        [JsonIgnore]
        [BsonIgnoreIfDefault]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Key { get; set; }

        public DefinedCondition Defined { get; set; }

        public override bool Pass(IFilter filter) =>
            throw new InvalidOperationException($"{nameof(MongoPayloadCondition)} 不可通過過濾器");
    }
}