using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Reactions
{
    [Union(0, typeof(AppPageReaction))]
    [Union(1, typeof(InternalBrowserReaction))]
    [Union(2, typeof(ExternalBrowserReaction))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Reaction
    {
        public string Path { get; set; }
    }
}