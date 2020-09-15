using System;

namespace V3Lib.Models.Conditions
{
    public struct StartDateTime : IConditionField
    {
        public DateTime DateTime { get; set; }
    }
}