using todo.domain.core;
using todo.domain.TodoItem;

namespace todo.domain.TodoCollection;

public class TodoCollectionAggregate
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string? Message { get; init; }
    private List<TodoItemEntity> _Tasks { get; init; }
    public IReadOnlyCollection<TodoItemEntity> Tasks
    {
        get => this._Tasks.AsReadOnly();
    }

    private TodoCollectionAggregate(
        string id,
        string title,
        string? message,
        IReadOnlyCollection<TodoItemEntity> tasks
    )
    {
        this.Id = id;
        this.Title = title;
        this.Message = message;
        this._Tasks = tasks.ToList();
    }

    public static Result<TodoCollectionAggregate> Create(
        string id,
        string title,
        string? message = null,
        IReadOnlyCollection<TodoItemEntity>? tasks = null
    )
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return new Error("Id is required");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return new Error("Title is required");
        }

        return new TodoCollectionAggregate(
            id: id,
            title: title,
            message: message,
            tasks: tasks ?? []
        );
    }

    public Result<TodoCollectionAggregate> AddTask(TodoItemEntity task)
    {
        if (this._Tasks.Any(t => t.Id == task.Id))
        {
            return new Error("Task already in the collection");
        }

        this._Tasks.Add(task);

        return this;
    }

    public Result<TodoCollectionAggregate> AddTasks(IEnumerable<TodoItemEntity> tasks)
    {
        foreach (var task in tasks)
        {
            if (this._Tasks.Any(t => t.Id == task.Id))
            {
                return new Error("Task already in the collection");
            }
        }

        var taskIds = tasks.Select(t => t.Id);
        if (taskIds.Distinct().Count() != taskIds.Count())
        {
            return new Error("Duplicate tasks in the collection");
        }

        this._Tasks.AddRange(tasks);
        return this;
    }
}
