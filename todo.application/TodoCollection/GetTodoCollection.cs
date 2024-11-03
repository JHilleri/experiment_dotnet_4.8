using MediatR;
using todo.application.TodoCollection.Abstractions;
using todo.domain.core;

namespace todo.application.TodoCollection;

public static class GetTodoCollection
{
    public record Query(string Id) : IRequest<Result<ResponseItem>>;

    public record ResponseItem(string Id, string Title, IEnumerable<TodoItem> Items);

    public record TodoItem(string Id, string Title, bool IsComplete);

    internal class Handler(ITodoCollectionRepository repository)
        : IRequestHandler<Query, Result<ResponseItem>>
    {
        public Task<Result<ResponseItem>> Handle(
            Query request,
            CancellationToken cancellationToken
        ) =>
            repository
                .GetTaskCollection(request.Id)
                .Then(collection => new ResponseItem(
                    collection.Id,
                    collection.Title,
                    collection.Tasks.Select(item => new TodoItem(
                        item.Id,
                        item.Title,
                        item.IsCompleted
                    ))
                ));
    }
}
