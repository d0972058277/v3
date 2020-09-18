using System.Collections.Generic;
using MessagePack;

namespace V3Lib.Models.Conditions
{
    public class CommunityStructs : List<CommunityStruct>, IConditionField { }

    [MessagePackObject(true)]
    public class Community : IConditionField
    {
        public string Id { get; set; }
    }

    public struct CommunityStruct : IConditionField
    {
        public string Id { get; set; }
    }
}