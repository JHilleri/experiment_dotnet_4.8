using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using todo.application;
using todo.infrastructure;

namespace todo.rest_4_8;

public class DependencyResolverWithServiceProvider(IServiceProvider serviceProvider)
    : IDependencyResolver
{
    public IDependencyScope BeginScope()
    {
        return new DependencyResolverWithServiceProvider(
            serviceProvider.CreateScope().ServiceProvider
        );
    }

    public void Dispose() { }

    public object GetService(Type serviceType)
    {
        return serviceProvider.GetService(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return serviceProvider
            .GetServices(serviceType)
            .Where((service => service != null))
            .Select(service => service!);
    }
}

public static class DependenciesConfig
{
    public static IServiceProvider GetResolver()
    {
        var services = new ServiceCollection();

        services
            .AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(ApplicationAssembly.Assembly)
            )
            .AddInfrastructureDependencies()
            .AddLogging(action => action.AddCustomLogger().AddDebug())
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
