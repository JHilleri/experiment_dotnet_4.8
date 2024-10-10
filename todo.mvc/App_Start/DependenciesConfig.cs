using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using todo.application;
using todo.infrastructure;

namespace todo.mvc.App_Start;

public class DependenciesConfig
{
    public static void RegisterDependencies()
    {
        var services = new ServiceCollection();

        services
            .AddApplicationDependencies()
            .AddInfrastructureDependencies()
            .AddLogging(action => action.AddCustomLogger())
            .AddControllersAsServices(
                typeof(DependenciesConfig)
                    .Assembly.GetExportedTypes()
                    .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                    .Where(t =>
                        typeof(IController).IsAssignableFrom(t)
                        || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                    )
            );

        IServiceProvider provider = services.BuildServiceProvider();
        DependencyResolverWithServiceProvider dependencyResolver = new(provider);
        DependencyResolver.SetResolver(dependencyResolver);
    }
}
