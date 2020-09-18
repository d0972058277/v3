using MessagePack;
using V3Lib.Filters.Abstractions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Conditions
{
    [Union(0, typeof(DefinedCondition))]
    [Union(1, typeof(ReferenceCondition))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Condition
    {
        public abstract bool Pass(IFilter filter);
    }
}