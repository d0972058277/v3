using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using V3Lib.Models;
using V3Lib.Models.Conditions;
using V3Lib.Strategies;
using V3Lib.Strategies.Abstractions;

namespace V3WebApi.Controllers.Conditions
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public partial class ConditionController : ControllerBase
    {
        protected IMongoClient _mongoClient;
        protected IConfigConditStrategy _configConditStrategy;
        protected IConfigConditsStrategy _configConditsStrategy;

        public ConditionController(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _configConditStrategy = new MongoConfigConditStrategy(_mongoClient, "Condition", "Defined");
            _configConditsStrategy = new MongoConfigConditsStrategy(_mongoClient, "Condition", "Defined");
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Defined")]
        public async Task<ActionResult<List<ConfigCondition>>> GetDefineds()
        {
            var result = await _configConditsStrategy.GetAsync();
            return Ok(result);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Defined")]
        public async Task<ActionResult> PostDefined([FromBody] List<ConfigCondition> configs)
        {
            await _configConditsStrategy.SetAsync(configs);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Defined")]
        public async Task<ActionResult> PutDefined([FromBody] List<ConfigCondition> configs)
        {
            await _configConditsStrategy.SetAsync(configs);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Defined")]
        public async Task<ActionResult> DeleteDefined()
        {
            await _configConditsStrategy.RemoveAsync();
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Defined/{key}")]
        public async Task<ActionResult<DefinedCondition>> GetDefined([FromRoute] string key)
        {
            _configConditStrategy.Key = key;
            var condigCondition = await _configConditStrategy.GetAsync();
            var result = condigCondition.Defined;
            return Ok(result);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Defined/{key}")]
        public async Task<ActionResult> PostDefined([FromRoute] string key, [FromBody] DefinedCondition defined)
        {
            var config = new ConfigCondition { Key = key, Defined = default };
            await _configConditStrategy.SetAsync(config);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Defined/{key}")]
        public async Task<ActionResult> PutDefined([FromRoute] string key, [FromBody] DefinedCondition defined)
        {
            var config = new ConfigCondition { Key = key, Defined = default };
            await _configConditStrategy.SetAsync(config);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Defined/{key}")]
        public async Task<ActionResult> DeleteDefined([FromRoute] string key)
        {
            _configConditStrategy.Key = key;
            await _configConditStrategy.RemoveAsync();
            return Ok();
        }
    }
}