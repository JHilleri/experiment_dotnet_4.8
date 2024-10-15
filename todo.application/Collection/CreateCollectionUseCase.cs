using todo.application.core;
using todo.domain.Collection;
using todo.domain.core;

namespace todo.application.Collection;

public record CreateCollection(string Title) : IUseCase<Result<string>>;

[Injectable]
public class CreateCollectionUseCase(ITaskCollectionRepository taskCollectionRepository)
    : IUseCaseImplementation<CreateCollection, Result<string>>
{
    public async Task<Result<string>> Execute(CreateCollection request)
    {
        return await TaskCollectionAggregate
            .Create(id: Guid.NewGuid().ToString(), title: request.Title)
            .Then(taskCollectionRepository.SaveTaskCollection)
            .CatchException((Exception ex) => new Error(ex.Message));
    }
}
