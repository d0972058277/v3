using V3Lib.Filters.Abstractions;
using V3Lib.Models;

namespace V3Lib.Filters
{
    public class NullFilter : IFilter
    {
        public bool Filter(IConditionField conditionField) => true;
    }
}