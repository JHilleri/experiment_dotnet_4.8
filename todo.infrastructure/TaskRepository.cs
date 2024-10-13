using todo.application.Abstractions;
using todo.application.core;
using todo.domain.Entities;

namespace todo.infrastructure;

[Injectable(Lifetime.Singleton)]
public class TaskRepository : ITaskRepository
{
    private readonly Dictionary<string, TaskEntity> TaskEntities = [];

    public async Task<TaskEntity?> GetTask(string id)
    {
        await Task.Delay(2);
        return this.TaskEntities.TryGetValue(id, out var task) ? task : null;
    }

    public async Task<IEnumerable<TaskEntity>> GetTasks()
    {
        await Task.Delay(2);
        return this.TaskEntities.Values;
    }

    public async Task SaveTask(TaskEntity task)
    {
        await Task.Delay(2);
        this.TaskEntities[task.Id] = task;
    }
}
