using todo.domain.Todo;

namespace todo.domain.Collection;

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
        var newTasks = this.Tasks.Concat(tasks).ToList().AsReadOnly();
        return this with { Tasks = newTasks };
    }
}