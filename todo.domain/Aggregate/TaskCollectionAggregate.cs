#nullable enable
using System.Collections.Generic;
using System.Linq;
using todo.domain.Entities;

namespace todo.domain.Aggregate;

public record TaskCollectionAggregate(
    string Id,
    string Title,
    string? Message,
    IReadOnlyCollection<TaskEntity> Tasks
)
{
    public TaskCollectionAggregate AddTask(TaskEntity task)
    {
        var newTasks = this.Tasks.Append(task).ToList().AsReadOnly();
        return this with { Tasks = newTasks };
    }

    public TaskCollectionAggregate AddTasks(IEnumerable<TaskEntity> tasks)
    {
        var newTasks = Enumerable.Concat(this.Tasks, tasks).ToList().AsReadOnly();
        return this with { Tasks = newTasks };
    }
}