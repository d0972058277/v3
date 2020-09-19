using System.Collections.Generic;
using V3Lib.Models.Conditions;

namespace V3Lib.Strategies.Abstractions
{
    public interface IConfigConditStrategy : IStrategy<ConfigCondition>
    {
        string Key { get; set; }
    }
    public interface IConfigConditsStrategy : IStrategy<List<ConfigCondition>> { }
}