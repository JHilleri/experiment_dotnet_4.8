using MediatR;
using Microsoft.Extensions.Logging;
using todo.application.TodoCollection.Abstractions;
using todo.domain.core;
using todo.domain.TodoCollection;

namespace todo.application.TodoCollection;

public static class CreateTodoCollection
{
    public record Command(string Title) : IRequest<Result<string>>;

    internal class Handler(
        ITodoCollectionRepository taskCollectionRepository,
        ILogger<Handler> logger
    ) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        ) =>
            await TodoCollectionAggregate
                .Create(id: Guid.NewGuid().ToString(), title: request.Title)
                .Then(collection =>
                    taskCollectionRepository.SaveTaskCollection(collection, cancellationToken)
                )
                .Tap(_ =>
                    logger.LogInformation("Created collection with title {Title}", request.Title)
                );
    }
}
