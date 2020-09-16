using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V3Lib.Models.Pages;

namespace V3WebApi.Controllers.Pages
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public partial class PageController : ControllerBase { }
}