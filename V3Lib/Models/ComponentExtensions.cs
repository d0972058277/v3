using V3Lib.Models.Components;

namespace V3Lib.Models
{
    public static class ComponentExtensions
    {
        public static Component ClearSubComponents(this Component component)
        {
            if (component is CompositeComponent)
            {
                (component as CompositeComponent).ClearListOfSubComponents();
            }
            return component;
        }
    }
}