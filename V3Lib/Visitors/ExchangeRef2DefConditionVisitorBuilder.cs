using System.Collections.Generic;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;

namespace V3Lib.Visitors
{
    public class ExchangeRef2DefConditionVisitorBuilder : VisitorBuilder<ExchangeRef2DefConditionVisitor, DefConditionParams>
    {
        public ExchangeRef2DefConditionVisitorBuilder() : base(new DefConditionParams()) { }
    }

    public static class ExchangeRef2DefConditionVisitorBuilderExtensions
    {
        public static ExchangeRef2DefConditionVisitorBuilder SetDefinedConditions(this ExchangeRef2DefConditionVisitorBuilder builder, Dictionary<string, DefinedCondition> conditions)
        {
            builder.SetParams(new DefConditionParams { Conditions = conditions });
            return builder;
        }
    }
}