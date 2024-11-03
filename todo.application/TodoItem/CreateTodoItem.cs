using MediatR;
using todo.application.Common.Abstractions;
using todo.application.TodoCollection.Abstractions;
using todo.application.TodoItem.Abstractions;
using todo.domain.core;
using todo.domain.TodoItem;

namespace todo.application.TodoItem;

public static class CreateTodoItem
{
    public record Command(
        string Title,
        string CollectionId,
        string? Message = null,
        DateTime? Deadline = null
    ) : IRequest<Result<string>>;

    public class Handler(
        IDateProvider dateProvider,
        ITodoItemRepository taskRepository,
        ITodoCollectionRepository taskCollectionRepository
    ) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        ) =>
            await TodoItemEntity
                .Create(
                    title: request.Title,
                    message: request.Message,
                    createdAt: dateProvider.Now,
                    deadline: request.Deadline
                )
                .Then(item =>
                    taskCollectionRepository
                        .GetTaskCollection(request.CollectionId)
                        .Then(collection => collection.AddTask(item))
                        .Tap(_ => taskRepository.SaveTask(item))
                        .Tap(collection =>
                            taskCollectionRepository.SaveTaskCollection(
                                collection,
                                cancellationToken
                            )
                        )
                        .Then(_ => item.Id)
                );
    }
}
