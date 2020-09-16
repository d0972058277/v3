using V3Lib.Creationals.Abstractions;
using V3Lib.Models.Params;

namespace V3Lib.Visitors
{
    public class FlatComponentVisitorBuilder : VisitorBuilder<FlatComponentVisitor, NullParams>
    {
        public FlatComponentVisitorBuilder() : base(new NullParams()) { }
    }
}