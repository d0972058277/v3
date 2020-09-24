using System;
using System.Collections.Generic;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class LinkComponentRelationVisitor : VisitorBase<Component>
    {
        public LinkComponentRelationVisitor(IParams @params) : base(@params) { }

        public Dictionary<Guid, Component> FlatElements { get; } = new Dictionary<Guid, Component>();

        public override void Visit(Component element)
        {
            FlatElements.Add(element.ComponentId, element);

            if (element.UpperLayerComponentId.HasValue)
            {
                var upperLayerComponent = FlatElements[element.UpperLayerComponentId.Value];
                element.SetUpperLayerComponent(upperLayerComponent);
            }
        }
    }

    public class LinkComponentRelationVisitorBuilder : VisitorBuilder<LinkComponentRelationVisitor, NullParams> { }
}