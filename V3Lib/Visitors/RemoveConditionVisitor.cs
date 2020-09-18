using V3Lib.Creationals.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class RemoveConditionVisitor : VisitorBase<Component>
    {
        public RemoveConditionVisitor(NullParams @params) : base(@params) { }

        public override void Visit(Component element)
        {
            element.Condition = null;
        }
    }

    public class RemoveConditionVisitorBuilder : VisitorBuilder<RemoveConditionVisitor, NullParams> { }
}