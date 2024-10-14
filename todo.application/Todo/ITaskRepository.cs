using todo.domain.Todo;

namespace todo.application.Todo;

public interface ITaskRepository
{
    Task<TaskEntity?> GetTask(string id);
    Task<IEnumerable<TaskEntity>> GetTasks();
    Task SaveTask(TaskEntity task);
}
