#nullable enable

using System.Collections.Generic;
using todo.application.Abstractions;
using todo.domain.Entities;

namespace todo.infrastructure;

public class TaskRepository : ITaskRepository
{
    private readonly Dictionary<string, TaskEntity> TaskEntities = [];

    public TaskEntity? GetTask(string id)
    {
        if (TaskEntities.ContainsKey(id))
        {
            return TaskEntities[id];
        }

        return null;
    }

    public IEnumerable<TaskEntity> GetTasks()
    {
        return TaskEntities.Values;
    }

    public void SaveTask(TaskEntity task)
    {
        if (TaskEntities.ContainsKey(task.Id))
        {
            TaskEntities[task.Id] = task;
        }
        else
        {
            TaskEntities.Add(task.Id, task);
        }
    }
}
