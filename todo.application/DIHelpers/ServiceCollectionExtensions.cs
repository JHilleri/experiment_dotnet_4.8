using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace todo.application.DIHelpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInjectableServices(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        var injectablesTypes = assembly
            .GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(InjectableAttribute), true).Length > 0);

        foreach (var type in injectablesTypes)
        {
            var injectableAttribute = type.GetCustomAttribute<InjectableAttribute>();
            if (injectableAttribute == null)
            {
                continue;
            }
            var interfaces = type.GetInterfaces().Where((i => i != typeof(IDisposable)));

            foreach (var interfacesType in interfaces)
            {
                switch (injectableAttribute.Lifetime)
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
