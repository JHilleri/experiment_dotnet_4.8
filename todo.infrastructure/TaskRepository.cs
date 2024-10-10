using todo.application.Abstractions;
using todo.domain.Entities;

namespace todo.infrastructure;

public class TaskRepository : ITaskRepository
{
    private readonly Dictionary<string, TaskEntity> TaskEntities = [];

    public TaskEntity? GetTask(string id)
    {
        if (this.TaskEntities.ContainsKey(id))
        {
            return this.TaskEntities[id];
        }

        return null;
    }

    public IEnumerable<TaskEntity> GetTasks()
    {
        return this.TaskEntities.Values;
    }

    public void SaveTask(TaskEntity task)
    {
        if (this.TaskEntities.ContainsKey(task.Id))
        {
            this.TaskEntities[task.Id] = task;
        }
        else
        {
            this.TaskEntities.Add(task.Id, task);
        }
    }
}
