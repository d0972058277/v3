using System.Collections.Generic;
using V3Lib.Creationals.Abstractions;
using V3Lib.Filters;
using V3Lib.Filters.Abstractions;
using V3Lib.Models.Conditions;
using V3Lib.Visitors;

namespace V3Lib.Creationals
{
    public class FilterComponentVisitorBuilder : IFilterComponentVisitorBuilder
    {
        public IFilter Filter { get; private set; } = new NoFilter();

        public FilterComponentVisitor Build() => new FilterComponentVisitor(Filter);

        public IFilterComponentVisitorBuilder SetFilter(IFilterDecorator filter)
        {
            Filter = filter;
            return this;
        }
    }

    public static class FilterComponentVisitorBuilderExtensions
    {
        public static IFilterComponentVisitorBuilder SetHideFilter(this IFilterComponentVisitorBuilder builder)
        {
            return builder.SetFilter(new HideFilter(builder.Filter));
        }

        public static IFilterComponentVisitorBuilder SetStartDateTimeFilter(this IFilterComponentVisitorBuilder builder)
        {
            return builder.SetFilter(new StartDateTimeFilter(builder.Filter));
        }

        public static IFilterComponentVisitorBuilder SetEndDateTimeFilter(this IFilterComponentVisitorBuilder builder)
        {
            return builder.SetFilter(new EndDateTimeFilter(builder.Filter));
        }

        public static IFilterComponentVisitorBuilder SetCommunityFilter(this IFilterComponentVisitorBuilder builder, List<Community> communities)
        {
            return builder.SetFilter(new CommunityFilter(builder.Filter, communities));
        }

        public static IFilterComponentVisitorBuilder SetLocationFilter(this IFilterComponentVisitorBuilder builder, List<Location> locations)
        {
            return builder.SetFilter(new LocationFilter(builder.Filter, locations));
        }
    }
}