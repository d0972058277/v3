using System;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public struct StartDateTime : IConditionField
    {
        public DateTime DateTime { get; set; }
    }
}