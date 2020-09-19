using System.Collections.Generic;
using System.Threading.Tasks;

namespace V3Lib.Strategies.Abstractions
{
    public interface IHistoryStrategy<T> : IStrategy<List<T>>
    {
        Task<T> PeekAsync();
        Task<T> PopAsync();
        Task PushAsync(T history);
    }
}