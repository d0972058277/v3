using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Styles;

namespace V3WebApi.Controllers.Pages
{
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("Admin/Home")]
        public async Task<ActionResult<AdminPageComponent>> GetAdminPageHome()
        {
            var page = new AdminPageComponent();

            var components = await _distributedCache.GetObjectAsync<List<Component>>(Page.Home.ToString());
            page.SubComponents = components;

            var conditions = await _distributedCache.GetObjectAsync<Dictionary<string, DefinedCondition>>("Conditions");
            page.Conditions = conditions;

            var styles = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Style)));
            foreach (var type in styles)
            {
                page.Styles.Add((Style) Activator.CreateInstance(type));
            }

            return Ok(page);
        }
    }
}