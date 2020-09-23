using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Histories;
using V3Lib.Models.Styles;
using V3Lib.Strategies;
using V3Lib.Strategies.Abstractions;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Pages
{
    public partial class HomeController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Admin")]
        public async Task<ActionResult<Component>> GetHomeAdmin()
        {
            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);
            var conditions = await _configConditsStrategy.GetAsync(_conditionDefined);

            var adminComponent = _mapper.Map<AdminPageComponent>(configComponent);

            adminComponent.Conditions = conditions.ToDictionary(c => c.Key, c => c.Defined);

            return Ok(adminComponent);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin")]
        public async Task<ActionResult> PostHomeAdmin([FromBody] ConfigPageComponent home)
        {
            await _mongoComponentStrategy.SetAsync(_componentHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Admin")]
        public async Task<ActionResult> PutHomeAdmin([FromBody] ConfigPageComponent home)
        {
            await _mongoComponentStrategy.SetAsync(_componentHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Admin")]
        public async Task<ActionResult> DeleteHomeAdmin()
        {
            await _mongoComponentStrategy.RemoveAsync(_componentHome);
            return Ok();

        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Apply")]
        public async Task<ActionResult> PostHomeAdminApply()
        {
            var currentHome = await _redisComponentStrategy.GetAsync(_cacheHome);
            var currentHistory = new ComponentHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Component = currentHome
            };
            await _historyStrategy.PushAsync(_componentHistoryHomeUndo, currentHistory);
            await _historyStrategy.RemoveAsync(_componentHistoryHomeRedo);

            var home = await _mongoComponentStrategy.GetAsync(_componentHome);
            await _redisComponentStrategy.SetAsync(_cacheHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Undo")]
        public async Task<ActionResult> PostHomeAdminUndo()
        {
            var configComponent = await _redisComponentStrategy.GetAsync(_cacheHome);
            var componentHistory = new ComponentHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Component = configComponent
            };
            await _historyStrategy.PushAsync(_componentHistoryHomeRedo, componentHistory);

            var home = await _historyStrategy.PopAsync(_componentHistoryHomeUndo);

            await _redisComponentStrategy.SetAsync(_cacheHome, home.Component);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Redo")]
        public async Task<ActionResult> PostHomeAdminRedo()
        {
            var configComponent = await _redisComponentStrategy.GetAsync(_cacheHome);
            var componentHistory = new ComponentHistory
            {
                Editor = new History.HistoryEditor { Name = "Test" },
                Component = configComponent
            };
            await _historyStrategy.PushAsync(_componentHistoryHomeUndo, componentHistory);

            var home = await _historyStrategy.PopAsync(_componentHistoryHomeRedo);

            await _redisComponentStrategy.SetAsync(_cacheHome, home.Component);
            return Ok();
        }
    }
}