using System;
using System.Collections.Generic;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class FlatComponentVisitor : VisitorBase<Component>, IFlatVisitor<Component>
    {
        public FlatComponentVisitor(NullParams @params) : base(@params) { }

        public Dictionary<Guid, Component> FlatElement { get; } = new Dictionary<Guid, Component>();

        public override void Visit(Component element)
        {
            FlatElement.Add(element.ComponentId, element);
        }
    }

    public class FlatComponentVisitorBuilder : VisitorBuilder<FlatComponentVisitor, NullParams> { }
}