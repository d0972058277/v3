using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Pages
{
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Admin/Home")]
        public async Task<ActionResult<List<Component>>> GetAdminPageHome()
        {
            // var builder = Builders<BsonDocument>.Filter;
            // var filter = builder.Empty;
            // var bsonDocument = await _mongoClient.GetDatabase("Component").GetCollection<BsonDocument>("Home").Find(filter).ToListAsync();

            // foreach (var component in bsonDocument)
            // {
            //     var test = BsonSerializer.Deserialize<Component>(component);
            // }

            var cbuilder = Builders<Component>.Filter;
            var cfilter = cbuilder.Empty;
            var components = await _mongoClient.GetDatabase("Component").GetCollection<Component>("Home").Find(cfilter).ToListAsync();

            return Ok(components);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Home")]
        public async Task<ActionResult> PostAdminPageHome([FromBody] ConfigPageComponent home)
        {
            var flatVisitor = _visitorFactory.GetVisitor<FlatComponentVisitor>();
            home.Accept(flatVisitor);
            var components = flatVisitor.FlatElements.Values.ToList();
            components.ForEach(component => component.ClearSubComponents());

            await _mongoClient.GetDatabase("Component").GetCollection<Component>("Home").InsertManyAsync(components);

            return Ok();
        }
    }
}