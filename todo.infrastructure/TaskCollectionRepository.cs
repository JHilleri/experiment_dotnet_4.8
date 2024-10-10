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
        if (this.TaskCollections.ContainsKey(id))
        {
            return this.TaskCollections[id];
        }

        return null;
    }

    public IEnumerable<TaskCollectionAggregate> GetTaskCollections()
    {
        return this.TaskCollections.Values;
    }

    public void SaveTaskCollection(TaskCollectionAggregate taskCollection)
    {
        if (taskCollection is { Title: "error" })
        {
            throw new Exception("Error: the title is 'error'.");
        }
        this.TaskCollections.Add(taskCollection.Id, taskCollection);
    }
}
