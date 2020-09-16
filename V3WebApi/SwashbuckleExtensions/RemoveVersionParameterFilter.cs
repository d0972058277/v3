using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace V3WebApi.SwashbuckleExtensions
{
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Any())
            {
                var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");

                if (versionParameter is null) return;

                operation.Parameters.Remove(versionParameter);
            }
        }
    }
}