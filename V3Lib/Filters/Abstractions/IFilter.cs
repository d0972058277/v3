using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3Lib.Filters.Abstractions
{
    public interface IFilter : IParams
    {
        bool Filter(IConditionField conditionField);
    }

    public interface IFilterDecorator : IFilter
    {
        IFilter InnerFilter { get; }
    }
}