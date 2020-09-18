using System;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public class StartDateTimeCondit : IConditionField
    {
        public DateTime DateTime { get; set; }
    }
}