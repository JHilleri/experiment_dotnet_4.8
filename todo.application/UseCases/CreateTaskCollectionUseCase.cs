using Microsoft.Extensions.Logging;
using todo.application.Abstractions;
using todo.application.Contracts;
using todo.domain.Aggregate;
using todo.domain.Entities;

namespace todo.application.UseCases;

public class CreateTaskCollectionUseCase(
    ITaskCollectionRepository taskCollectionRepository,
    ILogger<CreateTaskCollectionUseCase> logger
) : ICreateTaskCollectionUseCase
{
    public void CreateTaskCollection(string title)
    {
        try
        {
            var taskCollection = new TaskCollectionAggregate(
                Id: Guid.NewGuid().ToString(),
                Title: title,
                Tasks: new List<TaskEntity>().AsReadOnly(),
                Message: null
            );
            taskCollectionRepository.SaveTaskCollection(taskCollection);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create task collection");
        }
    }
}
