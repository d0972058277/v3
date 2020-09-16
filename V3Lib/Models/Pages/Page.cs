using System.Collections.Generic;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;

namespace V3Lib.Models
{
    public abstract class Page
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}