using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models;
using V3Lib.Models.Conditions;

namespace V3WebApi.Controllers.Conditions
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public partial class ConditionController : ControllerBase
    {
        protected IDistributedCache _distributedCache;

        public ConditionController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Defineds")]
        public async Task<ActionResult<Dictionary<string, DefinedCondition>>> GetDefineds()
        {
            var defineds = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>(DistributedCacheKeys.Conditions);
            if (defineds is null) defineds = new Dictionary<string, DefinedCondition>();

            return Ok(defineds);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Defineds/{key}")]
        public async Task<ActionResult<DefinedCondition>> GetDefined([FromRoute] string key)
        {
            var defineds = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>(DistributedCacheKeys.Conditions);
            if (defineds is null) defineds = new Dictionary<string, DefinedCondition>();

            var defined = defineds[key];

            return Ok(defined);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Defined/{key}")]
        public async Task<ActionResult> PostDefined([FromRoute] string key, [FromBody] DefinedCondition defined)
        {
            var defineds = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>(DistributedCacheKeys.Conditions);
            if (defineds is null) defineds = new Dictionary<string, DefinedCondition>();

            defineds.Add(key, defined);
            await _distributedCache.SetObjectAsync(DistributedCacheKeys.Conditions, defineds);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Defined/{key}")]
        public async Task<ActionResult> PutDefined([FromRoute] string key, [FromBody] DefinedCondition defined)
        {
            var defineds = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>(DistributedCacheKeys.Conditions);
            if (defineds is null) defineds = new Dictionary<string, DefinedCondition>();

            defineds[key] = defined;
            await _distributedCache.SetObjectAsync(DistributedCacheKeys.Conditions, defineds);

            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Defined/{key}")]
        public async Task<ActionResult<Dictionary<string, DefinedCondition>>> DeleteDefined([FromRoute] string key)
        {
            var defineds = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>(DistributedCacheKeys.Conditions);
            if (defineds is null) defineds = new Dictionary<string, DefinedCondition>();

            defineds.Remove(key);

            return Ok();
        }
    }
}