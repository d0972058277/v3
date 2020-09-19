using System;
using System.Collections.Generic;
using System.Linq;
using V3Lib.Models.Components;

namespace V3Lib.Models
{
    public static class ComponentExtensions
    {
        public static Component ClearComposite(this Component component)
        {
            if (component is CompositeComponent)
            {
                (component as CompositeComponent).ClearListOfSubComponents();
            }
            return component;
        }

        public static Component AddComposite(this Component component, Component subComponent)
        {
            if (component is CompositeComponent)
            {
                (component as CompositeComponent).AddLowerLayer(subComponent);
            }
            return component;
        }

        public static Component GetTree(this IEnumerable<Component> components)
        {
            var componentSet = components.ToDictionary(c => c.ComponentId, c => c);
            foreach (var component in components)
            {
                if (component.UpperLayerComponentId != null)
                {
                    var upper = componentSet[component.UpperLayerComponentId.Value];
                    upper.AddComposite(component);
                }
            }
            var result = components.Where(c => c.UpperLayerComponentId is null).Single();
            return result;
        }
    }
}