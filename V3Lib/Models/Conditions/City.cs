using System.Collections.Generic;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public class City : IConditionField
    {
        public string Name { get; set; }
        public HashSet<Area> Areas { get; set; } = new HashSet<Area>();
    }
}