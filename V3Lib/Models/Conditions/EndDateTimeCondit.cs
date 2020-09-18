using System;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public class EndDateTimeCondit : IConditionField
    {
        public DateTime DateTime { get; set; }
    }
}