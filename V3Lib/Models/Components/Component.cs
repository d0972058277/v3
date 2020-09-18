using System;
using System.Collections.Generic;
using MessagePack;
using V3Lib.Models.Conditions;
using V3Lib.NewtonsoftJsonExtensions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models.Components
{
    [Union(0, typeof(MemberComponent))]
    [Union(1, typeof(LazyComponent))]
    [MessagePackObject(true)]
    [AddJsonTypeName]
    public abstract class Component : IElement
    {
        protected Component _upperLayerComponent;

        public Guid? UpperLayerComponentId { get; set; } = null;

        public Guid ComponentId { get; set; } = Guid.NewGuid();

        public Condition Condition { get; set; }

        public Component GetRoot()
        {
            if (IsRoot()) return this;
            return _upperLayerComponent.GetRoot();
        }

        public bool IsRoot() => _upperLayerComponent is null;

        public virtual void Isolate()
        {
            SetUpperLayerComponent(null);
        }

        public void RemoveUpperLayer()
        {
            SetUpperLayerComponent(null);
        }

        public void SetUpperLayerComponent(Component component)
        {
            _upperLayerComponent = component;
            UpperLayerComponentId = component?.ComponentId;
        }

        public abstract void Accept(IVisitor visitor);
    }
}