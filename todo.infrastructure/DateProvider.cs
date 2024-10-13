using todo.application.Abstractions;
using todo.application.core;

namespace todo.infrastructure;

[Injectable]
public class DateProvider : IDateProvider
{
    public DateTime Now => DateTime.Now;
}
