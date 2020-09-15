using System.Collections.Generic;
using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3Lib.Filters
{
    public class CommunityFilter : IFilter, IFilterDecorator
    {
        public CommunityFilter(IFilter innerFilter, List<Community> communities)
        {
            InnerFilter = innerFilter;
            Communities = communities;
        }

        public IFilter InnerFilter { get; }

        public List<Community> Communities { get; }

        public bool Verify(IConditionField conditionField)
        {
            if (conditionField is Community)
            {
                var condition = (Community) conditionField;
                foreach (var community in Communities)
                {
                    if (community == condition)
                        return true;
                }
            }

            return InnerFilter.Verify(conditionField);
        }
    }
}