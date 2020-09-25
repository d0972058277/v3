using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace V3WebApi.Controllers
{
    [ApiVersionNeutral]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}