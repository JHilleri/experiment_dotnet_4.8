using todo.application.Abstractions;
using todo.application.DIHelpers;
using todo.domain.Entities;

namespace todo.infrastructure;

[Injectable(Lifetime.Singleton)]
public class TaskRepository : ITaskRepository
{
    private readonly Dictionary<string, TaskEntity> TaskEntities = [];

    public TaskEntity? GetTask(string id)
    {
        return this.TaskEntities.TryGetValue(id, out var task) ? task : null;
    }

    public IEnumerable<TaskEntity> GetTasks()
    {
        return this.TaskEntities.Values;
    }

    public void SaveTask(TaskEntity task)
    {
        this.TaskEntities[task.Id] = task;
    }
}
