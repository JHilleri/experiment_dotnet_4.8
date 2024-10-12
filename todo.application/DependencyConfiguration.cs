﻿using Microsoft.Extensions.DependencyInjection;
using todo.application.DIHelpers;

namespace todo.application;

public static class DependencyConfiguration
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddInjectableServices(typeof(DependencyConfiguration).Assembly);

        return services;
    }
}
