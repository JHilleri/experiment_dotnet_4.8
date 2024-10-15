namespace todo.domain.core;

public static class ResultExtensions
{
    public static Result<R> Then<T, R>(this Result<T> result, Func<T, R> func)
    {
        if (result.IsSuccess)
        {
            return func(result.GetValue());
        }
        return Result<R>.Failure(result.GetError());
    }

    public static async Task<Result<R>> Then<T, R>(this Result<T> result, Func<T, Task<R>> func)
    {
        if (result.IsSuccess)
        {
            return await func(result.GetValue());
        }
        return Result<R>.Failure(result.GetError());
    }

    public static async Task<Result<R>> Then<T, R>(this Task<Result<T>> result, Func<T, R> func) =>
        await result.Then(func);

    public static Result<T> Tap<T>(
        this Result<T> result,
        Action<T> onSuccess,
        Action<Error>? onError = null
    )
    {
        if (result.IsSuccess)
        {
            onSuccess(result.GetValue());
        }
        else
        {
            onError?.Invoke(result.GetError());
        }
        return result;
    }

    public static async Task<Result<T>> Tap<T>(
        this Task<Result<T>> result,
        Action<T> onSuccess,
        Action<Error>? onError = null
    ) => await result.Tap(onSuccess, onError);

    public static async Task<Result<T>> CatchException<T, E>(
        this Task<Result<T>> result,
        Func<E, Result<T>> func
    )
        where E : Exception
    {
        try
        {
            return await result;
        }
        catch (E e)
        {
            return func(e);
        }
    }
}
