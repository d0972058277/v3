using System.Collections.Generic;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;

namespace V3Lib.Models
{
    public class Page
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public List<Component> Components { get; set; }
        public Dictionary<string, DefinedCondition> Conditions { get; set; }
        public Dictionary<string, Style> Styles { get; set; }
    }
}