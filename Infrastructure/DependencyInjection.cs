
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Infrastructure.Implementations.Repositories;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Sarafi"));
                options.EnableDetailedErrors();
            },
            ServiceLifetime.Scoped);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
