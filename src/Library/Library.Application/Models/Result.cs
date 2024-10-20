namespace Library.Application.Models;

public class Result<T>
{
    private T Value { get; init; }
    private bool Success { get; init; }
    private string ErrorMessage { get; init; }

    private Result(T value)
    {
        Value = value;
        Success = true;
    }

    private Result(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Success = false;
    }

    public TOut Match<TOut>(Func<T, TOut> success, Func<string, TOut> failure) =>
        Success ? success(Value) : failure(ErrorMessage);

    public Result<T> Next(Func<T, Result<T>> next) => Success ? next(Value) : this;

    public Result<TOut> Map<TOut>(Func<T, TOut> mappingFunc) => Success ? mappingFunc(Value) : ErrorMessage;

    public async Task<Result<T>> Bind(Func<T, Task<Result<T>>> func) => Success ? await func(Value) : ErrorMessage;

    public static Result<T> From(T value) => new(value);

    public static implicit operator Result<T>(Exception exception) => new(exception.Message);

    public static implicit operator Result<T>(string errorMessage) => new(errorMessage);

    public static implicit operator Result<T>(T value) => new(value);
}

