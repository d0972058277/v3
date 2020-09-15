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

        public bool Verify(IConditionField conditionField)
        {
            if (conditionField is EndDateTime)
            {
                var condition = (EndDateTime) conditionField;
                if (condition.DateTime < DateTime.UtcNow.AddHours(8))
                    return true;
            }

            return InnerFilter.Verify(conditionField);
        }
    }
}