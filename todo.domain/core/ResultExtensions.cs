namespace todo.domain.core;

public static class ResultExtensions
{
    public static Result<R> Then<T, R>(this Result<T> result, Func<T, Result<R>> func)
    {
        if (result.IsSuccess)
        {
            return func(result.GetValue());
        }
        return Result<R>.Failure(result.GetError());
    }

    public static async Task<Result<R>> Then<T, R>(
        this Result<T> result,
        Func<T, Task<Result<R>>> func
    )
    {
        if (result.IsSuccess)
        {
            return await func(result.GetValue());
        }
        return Result<R>.Failure(result.GetError());
    }

    public static async Task<Result<R>> Then<T, R>(
        this Task<Result<T>> result,
        Func<T, Result<R>> func
    )
    {
        var unwrappedResult = await result;
        if (unwrappedResult.IsSuccess)
        {
            return func(unwrappedResult.GetValue());
        }
        return Result<R>.Failure(unwrappedResult.GetError());
    }

    public static async Task<Result<R>> Then<T, R>(this Task<Result<T>> result, Func<T, R> func)
    {
        var unwrappedResult = await result;
        if (unwrappedResult.IsSuccess)
        {
            return func(unwrappedResult.GetValue());
        }
        return Result<R>.Failure(unwrappedResult.GetError());
    }

    public static Task<Result<R>> Then<T, R>(
        this Task<Result<T>> result,
        Func<T, Task<Result<R>>> func
    ) => result.Then(func);

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
    ) => (await result).Tap(onSuccess, onError);

    public static Result<T> CatchError<T, E>(this Result<T> result, Func<E, Result<T>> func)
        where E : Error
    {
        if (!result.IsSuccess && result.GetError() is E e)
        {
            return func(e);
        }

        return result;
    }

    public static async Task<Result<T>> CatchError<T, E>(
        this Task<Result<T>> result,
        Func<E, Result<T>> func
    )
        where E : Error => (await result).CatchError(func);

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

    public static T Unwrap<T>(this Result<T> result, Func<Error, T> errorHandler)
    {
        if (result.IsSuccess)
        {
            return result.GetValue();
        }
        else
        {
            return errorHandler(result.GetError());
        }
    }

    public static async Task<T> Unwrap<T>(
        this Task<Result<T>> result,
        Func<Error, T> errorHandler
    ) => (await result).Unwrap(errorHandler);

    public static TOut Unwrap<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> successHandler,
        Func<Error, TOut> errorHandler
    )
    {
        if (result.IsSuccess)
        {
            return successHandler(result.GetValue());
        }

        return errorHandler(result.GetError());
    }

    public static async Task<TOut> Unwrap<TIn, TOut>(
        this Task<Result<TIn>> result,
        Func<TIn, TOut> successHandler,
        Func<Error, TOut> errorHandler
    ) => (await result).Unwrap(successHandler, errorHandler);
}
