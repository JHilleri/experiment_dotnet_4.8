using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

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
        return serviceProvider.GetServices(serviceType);
    }
}
