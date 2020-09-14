using System.Collections.Generic;

namespace V3Lib.Models.Conditions
{
    public struct City
    {
        public string Name { get; set; }
        public HashSet<Area> Areas { get; set; }
    }
}