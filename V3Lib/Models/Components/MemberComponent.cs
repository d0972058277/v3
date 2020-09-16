using System;
using System.Collections.Generic;
using System.Linq;
using V3Lib.Models.Conditions;
using V3Lib.Models.Extensions;
using V3Lib.Models.Medias;
using V3Lib.Models.Operations;
using V3Lib.Models.Reactions;
using V3Lib.Models.Styles;
using V3Lib.NewtonsoftJsonExtensions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models.Components
{
    [AddJsonTypeName]
    public class MemberComponent : Component
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        public Style Style { get; set; }

        public Extension Extension { get; set; }

        public Reaction Reaction { get; set; }

        public List<Media> Medias { get; set; } = new List<Media>();

        public List<Operation> Operations { get; set; } = new List<Operation>();

        public virtual List<Component> SubComponents { get; set; } = new List<Component>();

        public override void Accept(IVisitor visitor)
        {
            base.Accept(visitor);
            SubComponents.ForEach(component => component.Accept(visitor));
        }

        public override void AddLowerLayer(Component component)
        {
            component.SetUpperLayerComponent(this);
            SubComponents.Add(component);
        }

        public override void ClearLowerLayer() => SubComponents.Clear();

        public override bool Isolated() => _upperLayerComponent is null && !(SubComponents.Any());

        public override void LinkRelation2Extensions()
        {
            Style?.SetRelationComponent(this);
            Extension?.SetRelationComponent(this);
            Medias.ForEach(media => media.SetRelationComponent(this));
            Operations.ForEach(operation => operation.SetRelationComponent(this));
        }

        public override void LinkRelation2LowerLayers() => SubComponents.ForEach(component => component.SetUpperLayerComponent(this));

        public override void RemoveLowerLayer(Component component)
        {
            if (SubComponents.Contains(component))
            {
                component.RemoveUpperLayer();
                SubComponents.Remove(component);
            }
            else
            {
                throw new InvalidOperationException("SubComponents.Contains(component) is false.");
            }
        }
    }
}