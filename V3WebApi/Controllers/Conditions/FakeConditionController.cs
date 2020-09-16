using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib;
using V3Lib.Models;

namespace V3WebApi.Controllers.Conditions
{
    public partial class ConditionController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Fake/Defined")]
        public async Task<ActionResult> Fake()
        {
            var fake = FakeDataFactory.FakeDefinedConditions();
            await _distributedCache.SetObjectAsync(DistributedCacheKeys.Conditions, fake);
            return Ok();
        }
    }
}