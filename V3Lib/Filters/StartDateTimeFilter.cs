using System;
using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3Lib.Filters
{
    public class StartDateTimeFilter : IFilter, IFilterDecorator
    {
        public StartDateTimeFilter(IFilter innerFilter)
        {
            InnerFilter = innerFilter;
        }

        public IFilter InnerFilter { get; }

        public bool Filter(IConditionField conditionField)
        {
            if (conditionField is StartDateTimeCondit)
            {
                var condition = (StartDateTimeCondit) conditionField;
                if (condition.DateTime > DateTime.Now)
                    return false;
            }

            return InnerFilter.Filter(conditionField);
        }
    }
}