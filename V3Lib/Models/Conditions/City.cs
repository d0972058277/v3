using System.Collections.Generic;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public struct City : IConditionField
    {
        public string Name { get; set; }
        public HashSet<Area> Areas { get; set; }
    }
}