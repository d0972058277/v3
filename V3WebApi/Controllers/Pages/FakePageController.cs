using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
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
            var builder = Builders<MongoPayloadCondition>.Filter;
            var filter = builder.Empty;
            var defineds = await _mongoClient.GetDatabase("Condition").GetCollection<MongoPayloadCondition>("Defined").Find(filter).ToListAsync();
            var conditions = defineds.ToDictionary(d => d.Key, d => d.Defined);

            var page = new ConfigPageComponent().Fake();

            for (int i = 0; i < number; i++)
            {
                page.AddLowerLayer(default(Component).Fake(conditions, subNumber));
            }

            return Ok(page);
        }
    }
}