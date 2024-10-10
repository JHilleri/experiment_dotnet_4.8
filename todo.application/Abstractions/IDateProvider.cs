namespace todo.application.Abstractions;

public interface IDateProvider
{
    DateTime Now { get; }
}
