using Microsoft.Extensions.DependencyInjection;

namespace todo.application.core;

public class UseCaseService(IServiceProvider serviceProvider) : IUseCaseService
{
    public Task<Result> Execute<Result>(IUseCase<Result> request)
    {
        var useCaseImplementation = typeof(IUseCaseImplementation<,>).MakeGenericType(request.GetType(), typeof(Result));

        var resolved =
            serviceProvider.GetRequiredService(useCaseImplementation)
            ?? throw new InvalidOperationException(
                $"Implementation not found for use case {request.GetType()}, useCaseImplementation: {useCaseImplementation.GetType()}"
            );

        //if (resolved is IUseCase<IUseCaseParam<Result>, Result> useCase)
        //{
        //    return useCase.Execute(request);
        //}
        //else
        //{
        //    throw new InvalidOperationException(
        //        $"Resolved service is not of expected type IUseCase<IUseCaseParam<{typeof(Result)}>, {typeof(Result)}> but :{resolved}"
        //    );
        //}

        dynamic useCase = resolved;

        return useCase.Execute((dynamic)request);
    }

    public Task Execute<Params>(Params request)
        where Params : IUseCase
    {
        return
            serviceProvider.GetRequiredService<IUseCaseImplementation<Params>>() is not IUseCaseImplementation<Params> useCase
            ? throw new InvalidOperationException(
                $"Implementation not found for use case {request.GetType()}"
            )
            : useCase.Execute(request);
    }
}
