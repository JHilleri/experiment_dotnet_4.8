using System.Xml;
using todo.domain.core;

namespace todo.domain.TodoItem;

public sealed class TodoItemEntity
{
    public string Id { get; private set; }
    public string Title { get; private set; }
    public string? Message { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? Deadline { get; private set; }
    public List<TodoItemEntity> SubTasks { get; private set; }

    private TodoItemEntity(
        string id,
        string title,
        string? message,
        bool isCompleted,
        DateTime createdAt,
        DateTime? deadline,
        IEnumerable<TodoItemEntity> subTasks
    )
    {
        this.Id = id;
        this.Title = title;
        this.Message = message;
        this.IsCompleted = isCompleted;
        this.CreatedAt = createdAt;
        this.Deadline = deadline;
        this.SubTasks = subTasks.ToList();
    }

    public static Result<TodoItemEntity> Create(
        string title,
        string? message,
        DateTime createdAt,
        DateTime? deadline,
        IEnumerable<TodoItemEntity>? subTasks = null,
        bool isCompleted = false
    )
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return new Error($"The title must be defined");
        }
        return new TodoItemEntity(
            id: Guid.NewGuid().ToString(),
            title: title,
            message: message,
            isCompleted: isCompleted,
            createdAt: createdAt,
            deadline: deadline,
            subTasks: subTasks ?? []
        );
    }

    public TodoItemEntity AddSubTask(TodoItemEntity subTask)
    {
        this.SubTasks.Add(subTask);
        return this;
    }

    public TodoItemEntity AddSubTasks(IEnumerable<TodoItemEntity> subTasks)
    {
        this.SubTasks.AddRange(subTasks);
        return this;
    }
}
