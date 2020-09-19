using System;
using System.Collections.Generic;
using System.Linq;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;

namespace V3Lib.Models.Components
{
    public class AdminPageComponent : ConfigPageComponent
    {
        public Dictionary<string, DefinedCondition> Conditions { get; set; } = new Dictionary<string, DefinedCondition>();

        public List<Style> Styles =>
            AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.DefinedTypes
                .Where(type =>
                    typeof(Style).IsAssignableFrom(type) &&
                    type.IsClass &&
                    !type.IsAbstract))
            .Select(style => (Style) Activator.CreateInstance(style))
            .ToList();
    }
}