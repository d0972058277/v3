using System.Net.Http;
using V3Lib.Models.Styles;
using V3Lib.NewtonsoftJsonExtensions;

namespace V3Lib.Models.Components
{
    [AddJsonTypeName]
    public class LazyComponent : Component
    {
        public HttpMethod HttpMethod { get; } = HttpMethod.Get;

        public string Path { get; set; }

        public override void AddLowerLayer(Component component) { }

        public override void ClearLowerLayer() { }

        public override bool Isolated() => _upperLayerComponent is null;

        public override void LinkRelation2Extensions() { }

        public override void LinkRelation2LowerLayers() { }

        public override void RemoveLowerLayer(Component component) { }
    }
}