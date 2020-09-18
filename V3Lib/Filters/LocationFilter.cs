using System.Collections.Generic;
using System.Linq;
using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3Lib.Filters
{
    public class LocationFilter : IFilter, IFilterDecorator
    {
        public LocationFilter(IFilter innerFilter, List<Location> locations)
        {
            InnerFilter = innerFilter;
            Locations = locations;
        }

        public IFilter InnerFilter { get; }

        public List<Location> Locations { get; }

        public bool Filter(IConditionField conditionField)
        {
            if (conditionField is Location)
            {
                var conditions = (Locations) conditionField;
                if (!Locations.Any(location => conditions.Contains(location)))
                    return false;
            }

            return InnerFilter.Filter(conditionField);
        }
    }
}