using System.Net.Http;

namespace V3Lib.Models.Components
{
    public class LazyComponent : Component
    {
        public HttpMethod HttpMethod { get; } = HttpMethod.Get;

        public string Path { get; set; }
    }
}