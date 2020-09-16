using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Reactions
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class ExternalBrowserReaction : Reaction
    {

    }
}