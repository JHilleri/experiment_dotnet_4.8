namespace todo.application.core;

public interface IUseCaseService
{
    Task Execute<Params>(Params request)
        where Params : IUseCaseParam;
    Task<Result> Execute<Result>(IUseCaseParam<Result> request);
}
