using System.Collections.Generic;
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

        public bool Verify(IConditionField conditionField)
        {
            if (conditionField is Location)
            {
                var condition = (Location) conditionField;
                foreach (var location in Locations)
                {
                    if (location == condition)
                        return true;
                }
            }

            return InnerFilter.Verify(conditionField);
        }
    }
}