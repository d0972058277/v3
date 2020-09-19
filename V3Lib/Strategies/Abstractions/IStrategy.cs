using System.Threading.Tasks;
using V3Lib.Models.Components;

namespace V3Lib.Strategies.Abstractions
{
    public interface IStrategy<T>
    {
        Task<T> GetAsync();
        Task SetAsync(T component);
        Task RemoveAsync();
    }
}