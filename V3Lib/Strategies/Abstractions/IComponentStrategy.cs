using V3Lib.Models.Components;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public interface IComponentStrategy<T> : IStrategy<T, Component> where T : IStrategyParams { }
}