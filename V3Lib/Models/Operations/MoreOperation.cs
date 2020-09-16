using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Operations
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class MoreOperation : Operation { }
}