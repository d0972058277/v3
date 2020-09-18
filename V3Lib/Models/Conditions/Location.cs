using System.Collections.Generic;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    public class Locations : List<Location>, IConditionField { }

    [MessagePackObject(true)]
    public struct Location : IConditionField
    {
        public string City { get; set; }
        public string Area { get; set; }
        public string Village { get; set; }
    }
}