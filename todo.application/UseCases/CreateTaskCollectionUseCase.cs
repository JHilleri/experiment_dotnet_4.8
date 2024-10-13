using Microsoft.Extensions.Logging;
using todo.application.Abstractions;
using todo.application.core;
using todo.domain.Aggregate;
using todo.domain.Entities;

namespace todo.application.UseCases;

public record CreateTaskCollectionParam(string Title) : IUseCaseParam { }

[Injectable]
public class CreateTaskCollectionUseCase(
    ITaskCollectionRepository taskCollectionRepository,
    ILogger<CreateTaskCollectionUseCase> logger
) : IUseCase<CreateTaskCollectionParam>
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
