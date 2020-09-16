using System;
using System.Collections.Generic;
using MessagePack;
using Newtonsoft.Json;
using V3Lib.Filters.Abstractions;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;
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

        public Guid Id { get; set; } = Guid.NewGuid();

        public Condition Condition { get; set; }

        public void SetUpperLayerComponent(Component component) => _upperLayerComponent = component;

        public virtual void Accept(IVisitor visitor) => visitor.Visit(this);

        public virtual void Trim()
        {
            _upperLayerComponent.RemoveLowerLayer(this);
            RemoveUpperLayer();
            ClearLowerLayer();
        }

        public virtual void RemoveUpperLayer() => _upperLayerComponent = null;

        public virtual Component GetRoot()
        {
            if (_upperLayerComponent is null) return this;
            return _upperLayerComponent.GetRoot();
        }

        public virtual void ExchangeRef2DefinedCondition(Dictionary<string, DefinedCondition> definedConditions)
        {
            if (Condition is ReferenceCondition)
            {
                var condit = (ReferenceCondition) Condition;
                Condition = definedConditions[condit.Ref].Clone();
            }
        }

        public abstract bool Isolated();

        public abstract void LinkRelation2Extensions();

        public abstract void LinkRelation2LowerLayers();

        public abstract void AddLowerLayer(Component component);

        public abstract void ClearLowerLayer();

        public abstract void RemoveLowerLayer(Component component);
    }
}