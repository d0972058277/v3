using V3Lib.Models.Histories;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public interface IConditionHistoryStrategy<T> : IHistoryStrategy<T, ConditionHistory> where T : IStrategyParams { }
}