using System.Collections.Generic;
using V3Lib.Creationals.Abstractions;
using V3Lib.Filters;
using V3Lib.Filters.Abstractions;
using V3Lib.Models.Conditions;

namespace V3Lib.Visitors
{
    public class FilterComponentVisitorBuilder : VisitorBuilder<FilterComponentVisitor, IFilter>
    {
        public FilterComponentVisitorBuilder() : base(new NullFilter()) { }
    }

    public static class FilterComponentVisitorBuilderExtensions
    {
        public static IVisitorBuilder<FilterComponentVisitor, IFilter> SetHideFilter(this IVisitorBuilder<FilterComponentVisitor, IFilter> builder)
        {
            return builder.SetParams(new HideFilter(builder.Params));
        }

        public static IVisitorBuilder<FilterComponentVisitor, IFilter> SetStartDateTimeFilter(this IVisitorBuilder<FilterComponentVisitor, IFilter> builder)
        {
            return builder.SetParams(new StartDateTimeFilter(builder.Params));
        }

        public static IVisitorBuilder<FilterComponentVisitor, IFilter> SetEndDateTimeFilter(this IVisitorBuilder<FilterComponentVisitor, IFilter> builder)
        {
            return builder.SetParams(new EndDateTimeFilter(builder.Params));
        }

        public static IVisitorBuilder<FilterComponentVisitor, IFilter> SetCommunityFilter(this IVisitorBuilder<FilterComponentVisitor, IFilter> builder, List<Community> communities)
        {
            return builder.SetParams(new CommunityFilter(builder.Params, communities));
        }

        public static IVisitorBuilder<FilterComponentVisitor, IFilter> SetLocationFilter(this IVisitorBuilder<FilterComponentVisitor, IFilter> builder, List<Location> locations)
        {
            return builder.SetParams(new LocationFilter(builder.Params, locations));
        }
    }
}