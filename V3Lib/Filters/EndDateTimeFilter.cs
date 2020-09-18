using System;
using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3Lib.Filters
{
    public class EndDateTimeFilter : IFilter, IFilterDecorator
    {
        public EndDateTimeFilter(IFilter innerFilter)
        {
            InnerFilter = innerFilter;
        }

        public IFilter InnerFilter { get; }

        public bool Filter(IConditionField conditionField)
        {
            if (conditionField is EndDateTimeCondit)
            {
                var condition = (EndDateTimeCondit) conditionField;
                if (condition.DateTime < DateTime.Now)
                    return false;
            }

            return InnerFilter.Filter(conditionField);
        }
    }
}