using V3Lib.Filters.Abstractions;

namespace V3Lib.Models.Conditions
{
    public abstract class Condition : Additional
    {
        public abstract void Pass(IFilter filter);
    }
}