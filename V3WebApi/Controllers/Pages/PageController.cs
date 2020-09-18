using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Creationals;

namespace V3WebApi.Controllers.Pages
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public partial class PageController : ControllerBase
    {
        protected VisitorFactory _visitorFactory;
        protected IDistributedCache _distributedCache;

        public PageController(IDistributedCache distributedCache, VisitorFactory visitorBuilderFactory)
        {
            _distributedCache = distributedCache;
            _visitorFactory = visitorBuilderFactory;
        }
    }
}