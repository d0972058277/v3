using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Extensions
{
    [Union(0, typeof(ExtensionBase))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Extension : Additional<Extension> { }
}