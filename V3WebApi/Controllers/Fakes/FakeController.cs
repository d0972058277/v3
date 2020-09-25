using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Params;
using V3Lib.Strategies;

namespace V3WebApi.Controllers.Fakes
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FakeController : ControllerBase
    {
        protected MongoConfigConditStrategy _configConditsStrategy;
        protected MongoStrategyParams _conditionDefined;

        public FakeController(MongoConfigConditStrategy configConditsStrategy)
        {
            _configConditsStrategy = configConditsStrategy;

            _conditionDefined = new MongoStrategyParams { Database = "Condition", Collection = "Defined" };
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Exception")]
        public void GetException() =>
            throw new Exception("Test Exception.");

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Condition")]
        public ActionResult<List<ConfigCondition>> FakeCondition([FromQuery] int number)
        {
            var conditions = new List<ConfigCondition>();
            for (int i = 0; i < number; i++)
            {
                var fake = new DefinedCondition().Fake();
                var config = new ConfigCondition { Key = i.ToString(), Defined = fake };
                conditions.Add(config);
            }
            return Ok(conditions);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("ConfigComponent")]
        public async Task<ActionResult<ConfigPageComponent>> FakeConfigComponent([FromQuery] int number, [FromQuery] int subNumber)
        {
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Empty;
            var defineds = await _configConditsStrategy.GetAsync(_conditionDefined);
            var conditions = defineds.ToDictionary(d => d.Key, d => d.Defined);

            var page = new ConfigPageComponent().Fake();

            for (int i = 0; i < number; i++)
            {
                page.AddLowerLayer(default(Component).Fake(conditions, subNumber));
            }

            return Ok(page);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("MemberComponent")]
        public async Task<ActionResult<ConfigPageComponent>> FakeMemberComponent()
        {
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Empty;
            var defineds = await _configConditsStrategy.GetAsync(_conditionDefined);
            var conditions = defineds.ToDictionary(d => d.Key, d => d.Defined);

            var page = new MemberComponent().Fake(conditions);

            return Ok(page);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("LazyComponent")]
        public async Task<ActionResult<ConfigPageComponent>> FakeLazyComponent()
        {
            var builder = Builders<ConfigCondition>.Filter;
            var filter = builder.Empty;
            var defineds = await _configConditsStrategy.GetAsync(_conditionDefined);
            var conditions = defineds.ToDictionary(d => d.Key, d => d.Defined);

            var page = new LazyComponent().Fake(conditions);

            return Ok(page);
        }
    }
}