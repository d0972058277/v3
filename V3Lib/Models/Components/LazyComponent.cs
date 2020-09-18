using System.Collections.Generic;
using System.Net.Http;
using V3Lib.Models.Styles;
using V3Lib.NewtonsoftJsonExtensions;
using V3Lib.Visitors.Abstractions;

namespace V3Lib.Models.Components
{
    [AddJsonTypeName]
    public class LazyComponent : Component
    {
        public HttpMethod HttpMethod { get; } = HttpMethod.Get;

        public string Path { get; set; }

        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}