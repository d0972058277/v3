using System.Collections.Generic;

namespace V3Lib.Models.Conditions
{
    public struct Area
    {
        public string Name { get; set; }
        public HashSet<Village> Villages { get; set; }
    }
}