using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
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
        protected IMongoClient _mongoClient;

        protected IMongoCollection<MongoPayloadCondition> _collection => _mongoClient.GetDatabase("Conditions").GetCollection<MongoPayloadCondition>("Defined");

        public ConditionController(IDistributedCache distributedCache, IMongoClient mongoClient)
        {
            _distributedCache = distributedCache;
            _mongoClient = mongoClient;
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Defined")]
        public async Task<ActionResult<List<MongoPayloadCondition>>> GetDefineds()
        {
            var builder = Builders<MongoPayloadCondition>.Filter;
            var filter = builder.Empty;
            var cursor = await _collection.FindAsync(filter);
            var result = await cursor.ToListAsync();
            return Ok(result);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Defined")]
        public async Task<ActionResult> PostDefined([FromBody] List<MongoPayloadCondition> postDefineds)
        {
            await _collection.InsertManyAsync(postDefineds);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Defined")]
        public async Task<ActionResult> DeleteDefined()
        {
            var builder = Builders<MongoPayloadCondition>.Filter;
            var filter = builder.Empty;
            await _collection.DeleteManyAsync(filter);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Defined/{key}")]
        public async Task<ActionResult<MongoPayloadCondition>> GetDefined([FromRoute] string key)
        {
            var builder = Builders<MongoPayloadCondition>.Filter;
            var filter = builder.Eq(c => c.Key, key);
            var cursor = await _collection.FindAsync(filter);
            var result = await cursor.SingleAsync();
            return Ok(result);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Defined/{key}")]
        public async Task<ActionResult> PostDefined([FromRoute] string key, [FromBody] DefinedCondition defined)
        {
            var payload = new MongoPayloadCondition { Key = key, Defined = default };
            await _collection.InsertOneAsync(payload);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Defined/{key}")]
        public async Task<ActionResult> PutDefined([FromRoute] string key, [FromBody] DefinedCondition defined)
        {
            var payload = new MongoPayloadCondition { Key = key, Defined = default };
            var builder = Builders<MongoPayloadCondition>.Filter;
            var filter = builder.Eq(c => c.Key, key);
            var cursor = await _collection.FindOneAndReplaceAsync(filter, payload);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Defined/{key}")]
        public async Task<ActionResult<Dictionary<string, DefinedCondition>>> DeleteDefined([FromRoute] string key)
        {
            var builder = Builders<MongoPayloadCondition>.Filter;
            var filter = builder.Eq(c => c.Key, key);
            var cursor = await _collection.FindOneAndDeleteAsync(filter);
            return Ok();
        }
    }
}