using todo.domain.core;
using todo.domain.Todo;

namespace todo.domain.Collection;

public class TaskCollectionAggregate
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string? Message { get; init; }
    private List<TaskEntity> _Tasks { get; init; }
    public IReadOnlyCollection<TaskEntity> Tasks
    {
        get => this._Tasks.AsReadOnly();
    }

    private TaskCollectionAggregate(
        string id,
        string title,
        string? message,
        IReadOnlyCollection<TaskEntity> tasks
    )
    {
        this.Id = id;
        this.Title = title;
        this.Message = message;
        this._Tasks = tasks.ToList();
    }

    public static Result<TaskCollectionAggregate> Create(
        string id,
        string title,
        string? message = null,
        IReadOnlyCollection<TaskEntity>? tasks = null
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

        return new TaskCollectionAggregate(
            id: id,
            title: title,
            message: message,
            tasks: tasks ?? []
        );
    }

    public Result<TaskCollectionAggregate> AddTask(TaskEntity task)
    {
        if (this._Tasks.Any(t => t.Id == task.Id))
        {
            return new Error("Task already in the collection");
        }

        this._Tasks.Add(task);

        return this;
    }

    public Result<TaskCollectionAggregate> AddTasks(IEnumerable<TaskEntity> tasks)
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
