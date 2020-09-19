using System.Collections.Generic;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;

namespace V3Lib.Models.Components
{
    public class AdminPageComponent : ConfigPageComponent
    {
        public Dictionary<string, DefinedCondition> Conditions { get; set; } = new Dictionary<string, DefinedCondition>();

        public List<Style> Styles { get; set; } = new List<Style>();
    }
}