using System;
using V3Lib.Models.Conditions;

namespace V3Lib.Models.Components
{
    public abstract class Component
    {
        protected Component _upperLayerComponent;

        public Guid Id { get; set; }

        public virtual Condition Condition { get; set; }

        public void SetUpperLayerComponent(Component component) => _upperLayerComponent = component;

        public virtual void Trim()
        {
            TrimUpperLayer();
            TrimLowerLayer();
        }

        public virtual void TrimUpperLayer()
        {
            _upperLayerComponent.TrimLowerLayer();
            _upperLayerComponent = null;
        }

        public virtual void TrimLowerLayer() { }
    }
}