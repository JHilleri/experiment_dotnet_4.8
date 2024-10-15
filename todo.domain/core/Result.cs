namespace todo.domain.core;

public class Result<T>
{
    private T? Value { get; init; }
    protected Error? Error { get; init; }
    public bool IsSuccess { get; init; }

    private Result(T? value, Error? error, bool isSuccess)
    {
        this.Value = value;
        this.Error = error;
        this.IsSuccess = isSuccess;
    }

    public Error GetError()
    {
        if (this.IsSuccess)
        {
            throw new InvalidOperationException("Cannot get error from successful result");
        }
        return this.Error!;
    }

    public T GetValue()
    {
        if (!this.IsSuccess)
        {
            throw new InvalidOperationException("Cannot get value from failed result");
        }
        return this.Value!;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, null, true);
    }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(default, error, false);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Failure(error);
    }
}
