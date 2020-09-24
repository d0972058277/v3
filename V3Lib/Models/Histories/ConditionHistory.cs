using System.Collections.Generic;
using V3Lib.Models.Conditions;

namespace V3Lib.Models.Histories
{
    public class ConditionHistory : History
    {
        public List<ConfigCondition> Conditions { get; set; }
    }
}