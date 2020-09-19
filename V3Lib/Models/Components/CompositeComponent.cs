using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models.Components
{
    public abstract class CompositeComponent : Component
    {
        public virtual List<Component> SubComponents { get; set; } = new List<Component>();

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            SubComponents.ForEach(component => visitor.Visit(component));
        }

        public override void Isolate()
        {
            base.Isolate();
            RemoveLowerLayer();
        }

        public virtual void AddLowerLayer(Component component)
        {
            component.SetUpperLayerComponent(this);
            SubComponents.Add(component);
        }

        public virtual void ClearListOfSubComponents()
        {
            SubComponents.Clear();
        }

        public virtual void RemoveLowerLayer()
        {
            SubComponents.ToList().ForEach(component => RemoveLowerLayer(component));
        }

        public virtual void RemoveLowerLayer(Component component)
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