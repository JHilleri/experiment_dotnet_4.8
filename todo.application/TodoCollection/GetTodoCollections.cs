using MediatR;
using todo.application.TodoCollection.Abstractions;
using todo.domain.core;
using todo.domain.TodoCollection;

namespace todo.application.TodoCollection;

public static class GetTodoCollections
{
    public record Query() : IRequest<Result<IEnumerable<ResponseItem>>>;

    public record ResponseItem(string Id, string Title);

    internal class Handler(ITodoCollectionRepository taskCollectionRepository)
        : IRequestHandler<Query, Result<IEnumerable<ResponseItem>>>
    {
        public async Task<Result<IEnumerable<ResponseItem>>> Handle(
            Query request,
            CancellationToken cancellationToken
        ) =>
            await taskCollectionRepository
                .GetTaskCollections()
                .Then(results => results.Select(ToResponse));

        private static ResponseItem ToResponse(TodoCollectionAggregate collection) =>
            new(Id: collection.Id, Title: collection.Title);
    }
}
