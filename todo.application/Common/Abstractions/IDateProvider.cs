namespace todo.application.Common.Abstractions;

public interface IDateProvider
{
    DateTime Now { get; }
}
