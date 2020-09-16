using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V3Lib.Models.Components;
using V3Lib.Models.Conditions;
using V3Lib.Models.Pages;

namespace V3WebApi.Controllers.Pages
{
    public partial class PageController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet("User/Home")]
        public async Task<ActionResult<UserPage>> GetUserPageHome()
        {
            var p = new UserPage();

            return Ok(p);
        }
    }
}