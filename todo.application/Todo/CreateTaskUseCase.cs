using todo.application.Collection;
using todo.application.Common;
using todo.application.core;
using todo.domain.Todo;

namespace todo.application.Todo;

public record CreateTaskParams(
    string Title,
    string? Message,
    DateTime? Deadline,
    string CollectionId
) : IUseCase<string>;

[Injectable]
public class CreateTaskUseCase(
    IDateProvider dateProvider,
    ITaskRepository taskRepository,
    ITaskCollectionRepository taskCollectionRepository
) : IUseCaseImplementation<CreateTaskParams, string>
{
    public async Task<string> Execute(CreateTaskParams request)
    {
        string createdId = Guid.NewGuid().ToString();

        var task = new TaskEntity(
            Id: createdId,
            Title: request.Title,
            Message: request.Message,
            IsCompleted: false,
            CreatedAt: dateProvider.Now,
            Deadline: request.Deadline,
            SubTasks: new List<TaskEntity>().AsReadOnly()
        );
        var taskCollection =
            await taskCollectionRepository.GetTaskCollection(request.CollectionId)
            ?? throw new Exception("Task collection not found");

        var updatedTaskCollection = taskCollection.AddTask(task);

        await taskRepository.SaveTask(task);
        await taskCollectionRepository.SaveTaskCollection(updatedTaskCollection);

        return createdId;
    }
}
