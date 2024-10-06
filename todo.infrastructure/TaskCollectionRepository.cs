#nullable enable
using System.Collections.Generic;
using todo.application.Abstractions;
using todo.domain.Aggregate;

namespace todo.infrastructure;

public class TaskCollectionRepository : ITaskCollectionRepository
{
    private readonly Dictionary<string, TaskCollectionAggregate> TaskCollections = new()
    {
        { "1" , new TaskCollectionAggregate("1", "test", null, []) }
    };

    public TaskCollectionAggregate? GetTaskCollection(string id)
    {
        if (TaskCollections.ContainsKey(id))
        {
            return TaskCollections[id];
        }

        return null;
    }

    public IEnumerable<TaskCollectionAggregate> GetTaskCollections()
    {
        return TaskCollections.Values;
    }

    public void SaveTaskCollection(TaskCollectionAggregate taskCollection)
    {
        TaskCollections.Add(taskCollection.Id, taskCollection);
    }
}
