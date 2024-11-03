using todo.application.core;
using todo.application.TodoItem.Abstractions;
using todo.domain.TodoItem;

namespace todo.infrastructure.Persistence;

[Injectable(Lifetime.Singleton)]
public class TaskRepository : ITodoItemRepository
{
    private readonly Dictionary<string, TodoItemEntity> TaskEntities = [];

    public async Task<TodoItemEntity?> GetTask(string id)
    {
        await Task.Delay(2);
        return this.TaskEntities.TryGetValue(id, out var task) ? task : null;
    }

    public async Task<IEnumerable<TodoItemEntity>> GetTasks()
    {
        await Task.Delay(2);
        return this.TaskEntities.Values;
    }

    public async Task SaveTask(TodoItemEntity task)
    {
        await Task.Delay(2);
        this.TaskEntities[task.Id] = task;
    }
}
