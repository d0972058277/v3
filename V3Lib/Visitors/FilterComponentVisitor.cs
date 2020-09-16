using V3Lib.Filters.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class FilterComponentVisitor : VisitorBase<Component>, IFilterVisitor<Component>
    {
        public FilterComponentVisitor(IFilter filter) : base(filter) { }

        protected new IFilter Params { get; }

        public IFilter Filter { get => Params; }

        public override void Visit(Component element)
        {
            element.Condition.Pass(Filter);
        }
    }
}