using V3Lib.Filters.Abstractions;

namespace V3Lib.Models.Conditions
{
    public class NullCondition : Condition
    {
        public override void Pass(IFilter filter) { }
    }
}