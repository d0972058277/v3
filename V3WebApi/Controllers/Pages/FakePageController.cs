using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;

namespace V3WebApi.Controllers.Pages
{
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("FakePage")]
        public async Task<ActionResult<ConfigPageComponent>> Fake([FromQuery] int number, [FromQuery] int subNumber)
        {
            var conditions = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>("Conditions");
            var page = new ConfigPageComponent().Fake();

            for (int i = 0; i < number; i++)
            {
                page.AddLowerLayer(default(Component).Fake(conditions, subNumber));
            }

            return Ok(page);
        }
    }
}