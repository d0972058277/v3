using V3Lib.Models.Components;

namespace V3Lib.Models
{
    public abstract class Additional : IAdditional
    {
        protected Component _relationComponent;

        public virtual string TypeName => this.GetType().Name;

        public virtual void SetRelationComponent(Component component) => _relationComponent = component;

        public virtual Component GetRelationComponent() => _relationComponent;
    }
}