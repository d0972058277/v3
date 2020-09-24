using System;
using V3Lib.Creationals.Abstractions;
using V3Lib.Models.Components;
using V3Lib.Models.Params;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Visitors
{
    public class RemoveComponentVisitor : VisitorBase<Component>
    {
        public RemoveComponentVisitor(Params<Guid> @params) : base(@params) { }

        public Params<Guid> ComponentId => (Params<Guid>) _params;

        public override void Visit(Component element)
        {
            if (element.ComponentId == ComponentId.Value)
            {
                element.Isolate();
            }
        }
    }

    public class RemoveComponentVisitorBuilder : VisitorBuilder<RemoveComponentVisitor, Params<Guid>>
    {
        public RemoveComponentVisitorBuilder() : base()
        {
            Params = new Params<Guid>();
        }
    }

    public static class RemoveComponentVisitorBuilderExtensions
    {
        public static IVisitorBuilder<RemoveComponentVisitor, Params<Guid>> SetComponentId(this IVisitorBuilder<RemoveComponentVisitor, Params<Guid>> builder, Guid componentId)
        {
            builder.SetParams(new Params<Guid> { Value = componentId });
            return builder;
        }
    }
}