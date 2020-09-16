using System;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public struct EndDateTime : IConditionField
    {
        public DateTime DateTime { get; set; }
    }
}