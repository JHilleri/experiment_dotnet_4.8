namespace todo.application.core;

public interface IUseCaseParam<Result>;

public interface IUseCaseParam;

public interface IUseCase<in Request, Result>
    where Request : IUseCaseParam<Result>
{
    Task<Result> Execute(Request request);
}

public interface IUseCase<in Request>
    where Request : IUseCaseParam
{
    Task Execute(Request request);
}
