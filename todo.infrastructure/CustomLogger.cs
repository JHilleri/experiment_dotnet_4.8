using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using todo.application.Abstractions;

namespace todo.infrastructure;

public class EmptyScope : IDisposable
{
    public void Dispose() { }
}

public class CustomLogger(string name, IDateProvider dateProvider) : ILogger
{
    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        return new EmptyScope();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        if (!this.IsEnabled(logLevel))
        {
            return;
        }

        DateTime now = dateProvider.Now;
        string message = formatter(state, exception);
        if (exception is not null)
        {
            message += Environment.NewLine + exception;
        }
        string completeMessage = $"{now:u} {logLevel} [{name}] {message}";

        if (Debugger.IsAttached)
        {
            Debug.WriteLine(completeMessage);
        }

        Console.WriteLine(completeMessage);
    }
}

public class CustomLoggerProvider(IDateProvider dateProvider) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new CustomLogger(categoryName, dateProvider);
    }

    public void Dispose() { }
}

public static class CustomLoggerExtensions
{
    public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>()
        );
        return builder;
    }
}
