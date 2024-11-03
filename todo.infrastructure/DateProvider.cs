using todo.application.Common.Abstractions;
using todo.application.core;

namespace todo.infrastructure;

[Injectable]
public class DateProvider : IDateProvider
{
    public DateTime Now => DateTime.Now;
}
