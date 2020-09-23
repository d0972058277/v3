using V3Lib.Models.Histories;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public interface IComponentHistoryStrategy<T> : IHistoryStrategy<T, ComponentHistory> where T : IStrategyParams { }
}