using System.Collections.Generic;
using System.Threading.Tasks;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public interface IHistoryStrategy<T, Entity> : IStrategy<T, List<Entity>> where T : IStrategyParams
    {
        Task<Entity> PeekAsync(T strategyParams);
        Task<Entity> PopAsync(T strategyParams);
        Task PushAsync(T strategyParams, Entity history);
        Task DischargeAsync(T strategyParams);
    }
}