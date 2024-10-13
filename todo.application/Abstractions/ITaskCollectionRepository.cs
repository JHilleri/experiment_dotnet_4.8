using todo.domain.Aggregate;

namespace todo.application.Abstractions;

public interface ITaskCollectionRepository
{
    Task<TaskCollectionAggregate?> GetTaskCollection(string id);
    Task<IEnumerable<TaskCollectionAggregate>> GetTaskCollections();
    Task SaveTaskCollection(TaskCollectionAggregate taskCollection);
}
