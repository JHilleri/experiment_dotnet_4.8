using Microsoft.Extensions.Logging;
using todo.application.core;
using todo.domain.Collection;
using todo.domain.Todo;

namespace todo.application.Collection;

public record CreateTaskCollectionParam(string Title) : IUseCase;

[Injectable]
public class CreateTaskCollectionUseCase(
    ITaskCollectionRepository taskCollectionRepository,
    ILogger<CreateTaskCollectionUseCase> logger
) : IUseCaseImplementation<CreateTaskCollectionParam>
{
    public async Task Execute(CreateTaskCollectionParam request)
    {
        try
        {
            var taskCollection = new TaskCollectionAggregate(
                Id: Guid.NewGuid().ToString(),
                Title: request.Title,
                Tasks: new List<TaskEntity>().AsReadOnly(),
                Message: null
            );
            await taskCollectionRepository.SaveTaskCollection(taskCollection);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create task collection");
        }
    }
}
