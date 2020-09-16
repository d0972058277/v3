using System.Collections.Generic;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;

namespace V3Lib.Models.Pages
{
    public class AdminPage : Page
    {
        public Dictionary<string, DefinedCondition> Conditions { get; set; } = new Dictionary<string, DefinedCondition>();
        public List<Style> Styles { get; set; } = new List<Style>();
    }
}