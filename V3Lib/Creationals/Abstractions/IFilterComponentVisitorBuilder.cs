using V3Lib.Filters.Abstractions;
using V3Lib.Visitors;

namespace V3Lib.Creationals.Abstractions
{
    public interface IFilterComponentVisitorBuilder : IBuilder<FilterComponentVisitor>
    {
        IFilter Filter { get; }
        IFilterComponentVisitorBuilder SetFilter(IFilterDecorator filter);
    }
}