using todo.domain.Entities;

namespace todo.application.Abstractions;

public interface ITaskRepository
{
    TaskEntity? GetTask(string id);
    IEnumerable<TaskEntity> GetTasks();
    void SaveTask(TaskEntity task);
}
