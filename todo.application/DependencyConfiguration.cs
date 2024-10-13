using Microsoft.Extensions.DependencyInjection;
using todo.application.core;

namespace todo.application;

public static class DependencyConfiguration
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddInjectableServices(typeof(DependencyConfiguration).Assembly);
        services.AddScoped<IUseCaseService, UseCaseService>();

        return services;
    }
}
