using todo.domain.Aggregate;

namespace todo.application.Abstractions;

public interface ITaskCollectionRepository
{
    TaskCollectionAggregate? GetTaskCollection(string id);
    IEnumerable<TaskCollectionAggregate> GetTaskCollections();
    void SaveTaskCollection(TaskCollectionAggregate taskCollection);
}
