using V3Lib.Creationals.Abstractions;
using V3Lib.Models.Params;

namespace V3Lib.Visitors
{
    public class LinkRelationVisitorBuilder : VisitorBuilder<LinkRelationVisitor, NullParams>
    {
        public LinkRelationVisitorBuilder() : base(new NullParams()) { }
    }
}