using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace todo.application.core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInjectableServices(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        var injectablesTypes = assembly
            .GetTypes()
            .Where(t =>
                !t.IsGenericType
                && !t.IsAbstract
                && t.GetCustomAttributes(typeof(InjectableAttribute), false).Length > 0
            );

        foreach (var type in injectablesTypes)
        {
            var injectableAttribute = type.GetCustomAttribute<InjectableAttribute>();
            dynamic[] explicitTypes = (
                type.GetCustomAttributes(typeof(InjectableOfAttribute<>)) as dynamic[]
            )!;
            if (injectableAttribute is null)
            {
                continue;
            }
            var lifetime = injectableAttribute.Lifetime;

            var interfaces =
                explicitTypes?.Length > 0
                    ? explicitTypes.Select(t => t.Type as Type).ToArray()
                    : type.GetInterfaces().Where(i => i != typeof(IDisposable));

            foreach (var interfacesType in interfaces)
            {
                switch (lifetime)
                {
                    case Lifetime.Singleton:
                        services.AddSingleton(interfacesType, type);
                        break;
                    case Lifetime.Transient:
                        services.AddTransient(interfacesType, type);
                        break;
                    case Lifetime.Scoped:
                        services.AddScoped(interfacesType, type);
                        break;
                }
            }
        }

        return services;
    }
}
