using Microsoft.Extensions.Logging;
using todo.application.core;
using todo.application.TodoCollection.Abstractions;
using todo.domain.core;
using todo.domain.TodoCollection;

namespace todo.infrastructure.Persistence;

[Injectable(Lifetime.Singleton)]
public class TodoCollectionRepository(ILogger<TodoCollectionRepository> logger)
    : ITodoCollectionRepository
{
    private readonly Dictionary<string, TodoCollectionAggregate> TaskCollections = [];

    public async Task<Result<TodoCollectionAggregate>> GetTaskCollection(string id)
    {
        if (this.TaskCollections.ContainsKey(id))
        {
            await Task.Delay(2);
            return this.TaskCollections[id];
        }

        return new TodoCollectionNotFoundError(id);
    }

    public async Task<Result<IEnumerable<TodoCollectionAggregate>>> GetTaskCollections()
    {
        await Task.Delay(2);
        return this.TaskCollections.Values;
    }

    public async Task<Result<string>> SaveTaskCollection(
        TodoCollectionAggregate todoCollection,
        CancellationToken cancellationToken
    )
    {
        await Task.Delay(2);
        if (todoCollection is { Title: "error" })
        {
            logger.LogError(
                "Failed to create collection, invalid title {Title}",
                todoCollection.Title
            );
            return new InvalidCollectionNameError(todoCollection.Title);
        }
        this.TaskCollections.Add(todoCollection.Id, todoCollection);
        return todoCollection.Id;
    }
}
