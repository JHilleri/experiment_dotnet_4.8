using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using todo.application;
using todo.infrastructure;

namespace todo.rest_4_8;

public static class DependenciesConfig
{
    public static IServiceProvider GetResolver()
    {
        var services = new ServiceCollection();

        services
            .AddApplicationDependencies()
            .AddInfrastructureDependencies()
            .AddLogging(action => action.AddDebug())
            .AddControllersAsServices(
                typeof(DependenciesConfig)
                    .Assembly.GetExportedTypes()
                    .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                    .Where(t =>
                        typeof(IHttpController).IsAssignableFrom(t)
                        || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                    )
            );

        IServiceProvider provider = services.BuildServiceProvider();
        return provider;
    }

    public static IServiceCollection AddControllersAsServices(
        this IServiceCollection services,
        IEnumerable<Type> controllerTypes
    )
    {
        foreach (var type in controllerTypes)
        {
            services.AddTransient(type);
        }

        return services;
    }
}
