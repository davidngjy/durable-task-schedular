using System.Collections.Immutable;

namespace Domain.Abstractions;

public abstract record Result;

public record SuccessfulResult : Result
{
    public enum SuccessfulResultType
    {
        Ok,
        Created
    }

    public SuccessfulResultType Type { get; }

    protected SuccessfulResult(SuccessfulResultType type) => Type = type;

    public static SuccessfulResult Ok() => new(SuccessfulResultType.Ok);
    public static SuccessfulResult Created() => new(SuccessfulResultType.Created);
}

public record SuccessfulResult<T> : SuccessfulResult
{
    public T Content { get; private init; }

    private SuccessfulResult(SuccessfulResultType type, T content) : base(type) => Content = content;

    public static SuccessfulResult<T> Ok(T content) => new(SuccessfulResultType.Ok, content);
    public static SuccessfulResult<T> Created(T content) => new(SuccessfulResultType.Created, content);
}

public record FailureResult : Result
{
    public enum FailureResultType
    {
        NotFound,
        Conflicted,
        Validation
    }

    public string Title { get; }

    public string ErrorCode { get; }

    public string Description { get; }

    public FailureResultType Type { get; }

    public IReadOnlyDictionary<string, string> Detail { get; }

    private FailureResult(string title, string errorCode, string description, FailureResultType type, IReadOnlyDictionary<string, string>? detail)
    {
        Title = title;
        ErrorCode = errorCode;
        Description = description;
        Type = type;
        Detail = detail ?? ImmutableDictionary<string, string>.Empty;
    }

    public static FailureResult NotFound(string title, string errorCode, string description, IReadOnlyDictionary<string, string>? detail = null)
        => new(title, errorCode, description, FailureResultType.NotFound, detail);

    public static FailureResult Conflicted(string title, string errorCode, string description, IReadOnlyDictionary<string, string>? detail = null)
        => new(title, errorCode, description, FailureResultType.Conflicted, detail);

    public static FailureResult Validation(string title, string errorCode, string description, IReadOnlyDictionary<string, string>? detail = null)
        => new(title, errorCode, description, FailureResultType.Validation, detail);
}
