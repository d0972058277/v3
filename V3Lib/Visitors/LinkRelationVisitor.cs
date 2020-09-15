using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class LinkRelationVisitor : VisitorBase<Component>
    {
        public override void Visit(Component element)
        {
            element.LinkRelation2Extensions();
            element.LinkRelation2LowerLayers();
        }
    }
}