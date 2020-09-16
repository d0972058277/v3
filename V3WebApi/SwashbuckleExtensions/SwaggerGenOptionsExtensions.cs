using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace V3WebApi.SwashbuckleExtensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static SwaggerGenOptions SupportApiVersion(this SwaggerGenOptions c)
        {
            c.DocInclusionPredicate((version, apidescription) =>
            {
                if (apidescription.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controller)
                {
                    var versions = controller.ControllerTypeInfo.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
                    var maps = controller.MethodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == version) &&
                        (!maps.Any() || maps.Any(v => $"v{v.ToString()}" == version));;
                }
                return false;
            });
            return c;
        }
    }
}