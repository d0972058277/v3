using V3Lib.Models.Components;

namespace V3Lib.Models
{
    public abstract class Additional : IAdditional
    {
        protected Component _targetComponent;

        public virtual string TypeName => this.GetType().Name;

        public virtual void SetTargetComponent(Component component) => _targetComponent = component;

        public virtual Component GetTargetComponent() => _targetComponent;
    }
}