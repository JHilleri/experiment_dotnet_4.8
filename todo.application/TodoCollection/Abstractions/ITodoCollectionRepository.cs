using todo.domain.core;
using todo.domain.TodoCollection;

namespace todo.application.TodoCollection.Abstractions;

public interface ITodoCollectionRepository
{
    Task<Result<TodoCollectionAggregate>> GetTaskCollection(string id);
    Task<Result<IEnumerable<TodoCollectionAggregate>>> GetTaskCollections();
    Task<Result<string>> SaveTaskCollection(
        TodoCollectionAggregate todoCollection,
        CancellationToken cancellationToken
    );
}
