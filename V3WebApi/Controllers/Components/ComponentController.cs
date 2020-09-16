using Microsoft.AspNetCore.Mvc;
using V3Lib.Creationals;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Components
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ComponentController : ControllerBase
    {
        private VisitorBuilderFactory _visitorBuilderFactory;

        public ComponentController(VisitorBuilderFactory visitorBuilderFactory)
        {
            _visitorBuilderFactory = visitorBuilderFactory;
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Empty")]
        public void Empty()
        {
            var builder = _visitorBuilderFactory.Get<LinkRelationVisitorBuilder>();
        }
    }
}