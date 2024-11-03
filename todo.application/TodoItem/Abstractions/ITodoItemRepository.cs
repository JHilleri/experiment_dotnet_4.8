using todo.domain.TodoItem;

namespace todo.application.TodoItem.Abstractions;

public interface ITodoItemRepository
{
    Task<TodoItemEntity?> GetTask(string id);
    Task<IEnumerable<TodoItemEntity>> GetTasks();
    Task SaveTask(TodoItemEntity task);
}
