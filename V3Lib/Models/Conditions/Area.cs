using System.Collections.Generic;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    public class Area
    {
        public string Name { get; set; }
        public HashSet<Village> Villages { get; set; } = new HashSet<Village>();
    }
}