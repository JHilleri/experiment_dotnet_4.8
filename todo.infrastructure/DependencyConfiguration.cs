﻿using Microsoft.Extensions.DependencyInjection;
using todo.application.DIHelpers;

namespace todo.infrastructure;

public static class DependencyConfiguration
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddInjectableServices(typeof(DependencyConfiguration).Assembly);

        return services;
    }
}
