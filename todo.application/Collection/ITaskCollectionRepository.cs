using todo.domain.Collection;

namespace todo.application.Collection;

public interface ITaskCollectionRepository
{
    Task<TaskCollectionAggregate?> GetTaskCollection(string id);
    Task<IEnumerable<TaskCollectionAggregate>> GetTaskCollections();
    Task SaveTaskCollection(TaskCollectionAggregate taskCollection);
}
