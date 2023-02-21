using Sarafi.Application.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using Sarafi.API.Services;
using System.Text;
using NSwag.Generation.Processors.Security;
using NSwag;
using System.Text.Json;
using Sarafi.API.Filters;

namespace Sarafi.API;

public static class DependencyInjection
{
    public static void AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowedCorsOrigin", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
        services.AddOpenApiDocument(config =>
        {
            config.Title = "Sarafi Open API";
            config.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Paste JWT Here: Bearer {your JWT token}."
            });
            config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<ApiKeyOperationFilter>();
        });
        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(configuration["Jwt:IssuerSigninKey"])),
                ValidAudience = configuration["Jwt:ValidAudience"],
                ValidIssuer = configuration["Jwt:ValidIssuer"],
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ClockSkew = TimeSpan.Zero,
            };
            //// Configuration For Real-Time SignalR :
            //options.Events = new JwtBearerEvents
            //{
            //    OnMessageReceived = context => {
            //        var Token = context.Request.Query["token"];
            //        var path = context.HttpContext.Request.Path;
            //        if (!string.IsNullOrEmpty(Token) && (path.StartsWithSegments("/api/cacheHub") ||
            //                                             path.StartsWithSegments("/api/liveBoardHub") ||
            //                                             path.StartsWithSegments("/api/roleChangeDetectionHub")))
            //            context.Token = Token;
            //        return Task.CompletedTask;
            //    }
            //};
        });
    }
}
