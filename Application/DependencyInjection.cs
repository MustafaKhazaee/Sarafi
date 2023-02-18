
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sarafi.Application.Common.Behaviors;
using System.Reflection;

namespace Sarafi.Application;

public static class DependencyInjection
{
    public static void AddApplication (this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
    }
}
