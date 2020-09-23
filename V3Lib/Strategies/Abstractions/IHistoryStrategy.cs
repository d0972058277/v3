using System.Collections.Generic;
using System.Threading.Tasks;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public interface IHistoryStrategy<T, R> : IStrategy<T, List<R>> where T : IStrategyParams
    {
        Task<R> PeekAsync(T strategyParams);
        Task<R> PopAsync(T strategyParams);
        Task PushAsync(T strategyParams, R history);
        Task DischargeAsync(T strategyParams);
    }
}