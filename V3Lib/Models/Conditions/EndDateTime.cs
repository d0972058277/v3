using System;

namespace V3Lib.Models.Conditions
{
    public struct EndDateTime : IConditionField
    {
        public DateTime DateTime { get; set; }
    }
}