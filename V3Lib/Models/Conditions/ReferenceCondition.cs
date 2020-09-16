using System.Collections.Generic;
using V3Lib.Filters.Abstractions;

namespace V3Lib.Models.Conditions
{
    public class ReferenceCondition : Condition
    {
        public string Ref { get; set; }

        public override void Pass(IFilter filter) { }
    }
}