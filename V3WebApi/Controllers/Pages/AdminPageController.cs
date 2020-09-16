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
        [HttpGet("Admin/Home")]
        public async Task<ActionResult<AdminPage>> GetAdminPageHome()
        {
            var p = new AdminPage();

            return Ok(p);
        }
    }
}