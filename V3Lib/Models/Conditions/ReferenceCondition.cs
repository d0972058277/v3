using System.Collections.Generic;
using MessagePack;
using V3Lib.Filters.Abstractions;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Conditions
{
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public class ReferenceCondition : Condition
    {
        public string Ref { get; set; }

        public override void Pass(IFilter filter) { }
    }
}