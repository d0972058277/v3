using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class FilterComponentVisitor : VisitorBase<Component>, IFilterVisitor<Component>
    {
        public FilterComponentVisitor(IFilter filter)
        {
            Filter = filter;
        }

        public IFilter Filter { get; }

        public override void Visit(Component element)
        {
            element.Condition.Pass(Filter);
        }
    }
}