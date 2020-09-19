using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;
using V3Lib.Strategies;
using V3Lib.Strategies.Abstractions;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Pages
{
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Admin/Home")]
        public async Task<ActionResult<Component>> GetAdminPageHome()
        {
            var configComponent = await _componentStrategy.GetAsync();

            var adminComponent = _mapper.Map<AdminPageComponent>(configComponent);

            var conditionBuilder = Builders<ConfigCondition>.Filter;
            var conditionFilter = conditionBuilder.Empty;
            var conditions = await _mongoClient.GetDatabase("Condition").GetCollection<ConfigCondition>("Defined").Find(conditionFilter).ToListAsync();
            adminComponent.Conditions = conditions.ToDictionary(c => c.Key, c => c.Defined);

            adminComponent.Styles = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.DefinedTypes
                    .Where(type =>
                        typeof(Style).IsAssignableFrom(type) &&
                        type.IsClass &&
                        !type.IsAbstract))
                .Select(style => (Style) Activator.CreateInstance(style))
                .ToList();

            return Ok(adminComponent);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Home")]
        public async Task<ActionResult> PostAdminPageHome([FromBody] ConfigPageComponent home)
        {
            await _componentStrategy.SetAsync(home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Admin/Home")]
        public async Task<ActionResult> PutAdminPageHome([FromBody] ConfigPageComponent home)
        {
            await _componentStrategy.SetAsync(home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Admin/Home")]
        public async Task<ActionResult> DeleteAdminPageHome()
        {
            await _componentStrategy.RemoveAsync();
            return Ok();
        }
    }
}