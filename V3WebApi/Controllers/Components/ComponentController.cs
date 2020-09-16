using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Creationals;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Components
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public partial class ComponentController : ControllerBase
    {
        protected VisitorBuilderFactory _visitorBuilderFactory;
        protected IDistributedCache _distributedCache;

        public ComponentController(VisitorBuilderFactory visitorBuilderFactory, IDistributedCache distributedCache)
        {
            _visitorBuilderFactory = visitorBuilderFactory;
            _distributedCache = distributedCache;
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Fake")]
        public async Task<ActionResult> Fake()
        {
            var refCondit = new ReferenceCondition { Ref = "Ref" };
            var definedCondit = new DefinedCondition();

            var component = new MemberComponent
            {
                Title = "Title",
                SubTitle = "SubTitle",
                Description = "Description",
                Style = new BannerStyle()
            };

            var sub1 = new MemberComponent
            {
                Title = "Title",
                SubTitle = "SubTitle",
                Description = "Description",
                Style = new BannerStyle()
            };
            sub1.Condition = refCondit;

            var sub2 = new LazyComponent
            {
                Path = "Path"
            };
            sub2.Condition = definedCondit;

            component.SubComponents.Add(sub1);
            component.SubComponents.Add(sub2);

            await _distributedCache.SetObjectAsync(DistributedCacheKeys.Components, (Component) component);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet]
        public async Task<ActionResult<Component>> Gets()
        {
            var result = await _distributedCache.GetObjectAsync<Component>(DistributedCacheKeys.Components);
            return Ok(result);
        }
    }
}