using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Additionals
{
    [Union(0, typeof(MemberAdditional))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Additional { }
}