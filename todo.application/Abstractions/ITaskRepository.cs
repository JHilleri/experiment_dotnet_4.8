using todo.domain.Entities;

namespace todo.application.Abstractions;

public interface ITaskRepository
{
    Task<TaskEntity?> GetTask(string id);
    Task<IEnumerable<TaskEntity>> GetTasks();
    Task SaveTask(TaskEntity task);
}
