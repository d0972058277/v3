using System;
using System.Collections.Generic;
using System.Net.Http;
using V3Lib.Models.Styles;
using V3Lib.NewtonsoftJsonExtensions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models.Components
{
    public class LazyComponent : Component
    {
        public string HttpMethod { get; set; } = "Get";

        public string Path { get; set; }

        public override void Accept(IVisitor visitor) => visitor.Visit(this);

        protected override void RemoveLowerLayerComponent(Component component) { }
    }
}