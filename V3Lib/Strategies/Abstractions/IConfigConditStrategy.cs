using System.Collections.Generic;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;

namespace V3Lib.Strategies.Abstractions
{
    public interface IConfigConditStrategy<T> : IStrategy<T, ConfigCondition> where T : IStrategyParams { }
    public interface IConfigConditsStrategy<T> : IStrategy<T, List<ConfigCondition>> where T : IStrategyParams { }
}