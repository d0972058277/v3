using System.Collections.Generic;
using System.Linq;
using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3Lib.Filters
{
    public class CommunityFilter : IFilter, IFilterDecorator
    {
        public CommunityFilter(IFilter innerFilter, List<CommunityStruct> communityStructs)
        {
            InnerFilter = innerFilter;
            CommunityStructs = communityStructs;
        }

        public IFilter InnerFilter { get; }

        public List<CommunityStruct> CommunityStructs { get; }

        public bool Filter(IConditionField conditionField)
        {
            if (conditionField is CommunityStructs)
            {
                var conditions = (CommunityStructs) conditionField;
                if (!CommunityStructs.Any(community => conditions.Contains(community)))
                    return false;
            }

            return InnerFilter.Filter(conditionField);
        }
    }
}