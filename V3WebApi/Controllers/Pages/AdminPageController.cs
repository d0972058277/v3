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
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Admin/Home")]
        public async Task<ActionResult<Component>> GetAdminPageHome()
        {
            var configComponent = await _mongoComponentStrategy.GetAsync(_componentHome);
            var conditions = await _configConditsStrategy.GetAsync(_conditionDefined);

            var adminComponent = _mapper.Map<AdminPageComponent>(configComponent);

            adminComponent.Conditions = conditions.ToDictionary(c => c.Key, c => c.Defined);

            return Ok(adminComponent);
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Home")]
        public async Task<ActionResult> PostAdminPageHome([FromBody] ConfigPageComponent home)
        {
            await _mongoComponentStrategy.SetAsync(_componentHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPut("Admin/Home")]
        public async Task<ActionResult> PutAdminPageHome([FromBody] ConfigPageComponent home)
        {
            await _mongoComponentStrategy.SetAsync(_componentHome, home);
            return Ok();
        }

        [MapToApiVersion("3.0-patch0")]
        [HttpDelete("Admin/Home")]
        public async Task<ActionResult> DeleteAdminPageHome()
        {
            await _mongoComponentStrategy.RemoveAsync(_componentHome);
            return Ok();

        }

        [MapToApiVersion("3.0-patch0")]
        [HttpPost("Admin/Home/Apply")]
        public async Task<ActionResult> PostAdminPageHomeApply()
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
        [HttpPost("Admin/Home/Undo")]
        public async Task<ActionResult> PostAdminPageHomeUndo()
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
        [HttpPost("Admin/Home/Redo")]
        public async Task<ActionResult> PostAdminPageHomeRedo()
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