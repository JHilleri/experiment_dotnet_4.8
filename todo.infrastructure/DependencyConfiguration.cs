using Microsoft.Extensions.DependencyInjection;
using todo.application.Abstractions;

namespace todo.infrastructure
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddInfrastructureDependencies(
            this IServiceCollection services
        )
        {
            services.AddSingleton<ITaskRepository, TaskRepository>();
            services.AddSingleton<IDateProvider, DateProvider>();
            services.AddSingleton<ITaskCollectionRepository, TaskCollectionRepository>();

            return services;
        }
    }
}
