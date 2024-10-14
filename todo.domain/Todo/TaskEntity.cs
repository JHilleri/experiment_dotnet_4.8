namespace todo.domain.Todo;

public record TaskEntity(
    string Id,
    string Title,
    string? Message,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime? Deadline,
    IReadOnlyCollection<TaskEntity> SubTasks
)
{
    public TaskEntity AddSubTask(TaskEntity subTask)
    {
        return this with
        {
            SubTasks = new List<TaskEntity>(this.SubTasks) { subTask }.AsReadOnly(),
        };
    }

    public TaskEntity AddSubTasks(IEnumerable<TaskEntity> subTasks)
    {
        return this with
        {
            SubTasks = this.SubTasks.Concat(subTasks).ToList().AsReadOnly(),
        };
    }
}
