using todo.application.Collection;
using todo.application.core;
using todo.domain.Collection;

namespace todo.infrastructure;

[Injectable(Lifetime.Singleton)]
public class TaskCollectionRepository : ITaskCollectionRepository
{
    private readonly Dictionary<string, TaskCollectionAggregate> TaskCollections = [];

    public async Task<TaskCollectionAggregate?> GetTaskCollection(string id)
    {
        if (this.TaskCollections.ContainsKey(id))
        {
            await Task.Delay(2);
            return this.TaskCollections[id];
        }

        return null;
    }

    public async Task<IEnumerable<TaskCollectionAggregate>> GetTaskCollections()
    {
        await Task.Delay(2);
        return this.TaskCollections.Values;
    }

    public async Task<string> SaveTaskCollection(TaskCollectionAggregate taskCollection)
    {
        await Task.Delay(2);
        if (taskCollection is { Title: "error" })
        {
            throw new Exception("Error: the title is 'error'.");
        }
        this.TaskCollections.Add(taskCollection.Id, taskCollection);
        return taskCollection.Id;
    }
}
