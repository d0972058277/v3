using MessagePack;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Operations
{
    [Union(0, typeof(MoreOperation))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Operation : Additional<Operation> { }
}