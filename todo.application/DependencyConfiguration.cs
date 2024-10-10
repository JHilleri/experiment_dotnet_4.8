using Microsoft.Extensions.DependencyInjection;
using todo.application.Contracts;
using todo.application.UseCases;

namespace todo.application;

public static class DependencyConfiguration
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IGetCollectionsUseCase, GetCollectionsUseCase>();
        services.AddSingleton<ICreateTaskCollectionUseCase, CreateTaskCollectionUseCase>();

        return services;
    }
}
