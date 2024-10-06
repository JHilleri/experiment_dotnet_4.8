using todo.application.Abstractions;
using todo.application.Contracts;
using todo.application.UseCases;
using todo.infrastructure;

namespace todo.mvc.App_Start
{
    public class DependenciesConfig
    {
        public static void RegisterDependencies()
        {
            Dependencies dependencies = new();
            dependencies.RegisterSingleton<ITaskRepository>(() => new TaskRepository());
            dependencies.RegisterSingleton<IDateProvider>(() => new DateProvider());
            dependencies.RegisterSingleton<ITaskCollectionRepository>(
                () => new TaskCollectionRepository()
            );

            dependencies.Register<ICreateTaskCollectionUseCase>(
                () =>
                    new CreateTaskCollectionUseCase(
                        dependencies.Resolve<ITaskCollectionRepository>()
                    )
            );
            dependencies.Register<IGetCollectionsUseCase>(
                () => new GetCollectionsUseCase(dependencies.Resolve<ITaskCollectionRepository>())
            );
        }
    }
}
