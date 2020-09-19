using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using V3Lib.Models;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Visitors;

namespace V3WebApi.Controllers.Pages
{
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("User/Home")]
        public async Task<ActionResult<UserPageComponent>> GetUserPageHome()
        {
            var page = new UserPageComponent();

            return Ok(page);
        }
    }
}