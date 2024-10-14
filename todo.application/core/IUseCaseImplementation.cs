namespace todo.application.core;

public interface IUseCase<Result>;

public interface IUseCase;

public interface IUseCaseImplementation<in Request, Result>
    where Request : IUseCase<Result>
{
    Task<Result> Execute(Request request);
}

public interface IUseCaseImplementation<in Request>
    where Request : IUseCase
{
    Task Execute(Request request);
}
