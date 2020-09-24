using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using V3Lib.Models.Styles;

namespace V3WebApi.Controllers.Styles
{
    [ApiVersion("3.0-patch0")]
    [Produces("application/json", "application/x-msgpack")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class StyleController : ControllerBase
    {
        [MapToApiVersion("3.0-patch0")]
        [HttpGet]
        public ActionResult<List<Style>> GetStyles()
        {
            var styles = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.DefinedTypes
                    .Where(type =>
                        typeof(Style).IsAssignableFrom(type) &&
                        type.IsClass &&
                        !type.IsAbstract))
                .Select(style => (Style) Activator.CreateInstance(style))
                .ToList();
            return Ok(styles);
        }
    }
}