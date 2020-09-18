using System.Collections.Generic;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;
using V3Lib.Visitors.Abstractions;
using static V3Lib.Visitors.ExchangeRef2DefConditionVisitor;

namespace V3Lib.Visitors
{
    public class ExchangeRef2DefConditionVisitor : VisitorBase<Component>
    {
        public ExchangeRef2DefConditionVisitor(DefConditionParams defConditionparams) : base(defConditionparams) { }

        public DefConditionParams DefConditionParams => (DefConditionParams) _params;

        public Dictionary<string, DefinedCondition> Conditions => DefConditionParams.Conditions;

        public override void Visit(Component element)
        {
            if (element.Condition is ReferenceCondition)
            {
                var condit = (element.Condition as ReferenceCondition);
                element.Condition = Conditions[condit.Ref];
            }
        }
    }

    public class ExchangeRef2DefConditionVisitorBuilder : VisitorBuilder<ExchangeRef2DefConditionVisitor, DefConditionParams>
    {
        public ExchangeRef2DefConditionVisitorBuilder()
        {
            Params = new DefConditionParams();
        }
    }

    public static class ExchangeRef2DefConditionVisitorBuilderExtensions
    {
        public static IVisitorBuilder<ExchangeRef2DefConditionVisitor, DefConditionParams> SetDefinedConditions(this IVisitorBuilder<ExchangeRef2DefConditionVisitor, DefConditionParams> builder, Dictionary<string, DefinedCondition> conditions)
        {
            builder.SetParams(new DefConditionParams { Conditions = conditions });
            return builder;
        }
    }
}