using V3Lib.Filters.Abstractions;
using V3Lib.Models;

namespace V3Lib.Filters
{
    public class NoFilter : IFilter
    {
        public bool Verify(IConditionField conditionField) => false;
    }
}