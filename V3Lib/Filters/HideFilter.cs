using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3Lib.Filters
{
    public class HideFilter : IFilter, IFilterDecorator
    {
        public HideFilter(IFilter innerFilter)
        {
            InnerFilter = innerFilter;
        }

        public IFilter InnerFilter { get; }

        public bool Verify(IConditionField conditionField)
        {
            if (conditionField is Hide)
            {
                return true;
            }

            return InnerFilter.Verify(conditionField);
        }
    }
}