using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sarafi.API.Filters;

public class ApiKeyOperationFilter : IOperationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyOperationFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        var parameter = new OpenApiParameter()
        {
            Name = "x-api-key",
            In = ParameterLocation.Header,
            Style = ParameterStyle.Simple,
            Required = true,
            AllowEmptyValue = false
        };

#if DEBUG
        var apiKey = _configuration.GetValue<string>("x-api-key");
        parameter.Example = new OpenApiString(apiKey);
#endif

        operation.Parameters.Add(parameter);
    }
}
