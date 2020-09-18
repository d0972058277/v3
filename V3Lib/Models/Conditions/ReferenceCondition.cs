using System;
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

        public override bool Pass(IFilter filter) =>
            throw new InvalidOperationException($"{nameof(ReferenceCondition)} 不可通過過濾器");
    }
}