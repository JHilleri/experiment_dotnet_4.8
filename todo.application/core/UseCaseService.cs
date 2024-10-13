using Microsoft.Extensions.DependencyInjection;

namespace todo.application.core;

public class UseCaseService(IServiceProvider serviceProvider) : IUseCaseService
{
    public Task<Result> Execute<Result>(IUseCaseParam<Result> request)
    {
        var useCaseType = typeof(IUseCase<,>).MakeGenericType(request.GetType(), typeof(Result));

        var resolved =
            serviceProvider.GetRequiredService(useCaseType)
            ?? throw new InvalidOperationException(
                $"Use case not found for request {request.GetType()}, useCaseType: {useCaseType.GetType()}"
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
        where Params : IUseCaseParam
    {
        return
            serviceProvider.GetRequiredService<IUseCase<Params>>() is not IUseCase<Params> useCase
            ? throw new InvalidOperationException(
                $"Use case not found for request {request.GetType()}"
            )
            : useCase.Execute(request);
    }
}
