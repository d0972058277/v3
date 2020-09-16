using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class ExchangeRef2DefConditionVisitor : VisitorBase<Component>
    {
        public ExchangeRef2DefConditionVisitor(DefConditionParams defConditionparams) : base(defConditionparams) { }

        public DefConditionParams DefConditionParams => (DefConditionParams) _params;

        public override void Visit(Component element)
        {
            element.ExchangeRef2DefinedCondition(DefConditionParams.Conditions);
        }
    }
}