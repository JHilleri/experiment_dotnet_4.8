using System;
using todo.application.Abstractions;

namespace todo.infrastructure;

public class DateProvider : IDateProvider
{
    public DateTime Now => DateTime.Now;
}
