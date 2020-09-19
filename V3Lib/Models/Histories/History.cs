using System.Diagnostics.Contracts;
using MessagePack;
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
        public HistoryEditor Editor { get; set; }

        public class HistoryEditor
        {
            public string Name { get; set; }
        }
    }
}