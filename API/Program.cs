
using Sarafi.API.Middlewares;
using Sarafi.Application;
using Sarafi.Infrastructure;

namespace Sarafi.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddApi(builder.Configuration);

        var app = builder.Build();
        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"]?.ToString());

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerUi3();
            app.UseOpenApi();
        }

        app.UseMiddleware<ApiKeyMiddleware>();
        app.UseHttpsRedirection();
        app.UseCors("AllowedCorsOrigin");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
