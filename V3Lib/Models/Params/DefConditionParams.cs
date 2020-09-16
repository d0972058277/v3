using System.Collections.Generic;
using V3Lib.Models.Conditions;

namespace V3Lib.Models.Params
{
    public class DefConditionParams : IParams
    {
        public Dictionary<string, DefinedCondition> Conditions = new Dictionary<string, DefinedCondition>();
    }
}