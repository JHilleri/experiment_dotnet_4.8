namespace todo.application.core;

public interface IUseCaseService
{
    Task Execute<Params>(Params request)
        where Params : IUseCase;
    Task<Result> Execute<Result>(IUseCase<Result> request);
}
