using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace todo.mvc;

public class DependencyResolverWithServiceProvider(IServiceProvider serviceProvider)
    : IDependencyResolver
{
    public object GetService(Type serviceType)
    {
        return serviceProvider.GetService(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return serviceProvider.GetServices(serviceType).Where(service => service != null);
    }
}
