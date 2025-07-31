namespace Core.Domain.Abstractions;
public sealed class DomainResult<TValue>
{

    public bool IsSuccess { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public List<string> Errors { get; private set; } = [];

    private TValue? _value;
    public TValue Value => IsSuccess && _value is not null ? _value : EmptyValue();

    public int UpdatedFieldCount { get; private set; }

    private DomainResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    private TValue EmptyValue()
    {
        return _value = default!;
    }

    public DomainResult<TValue> WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public DomainResult<TValue> WithUpdatedFieldCount(int count)
    {
        UpdatedFieldCount = count;
        return this;
    }

    public static DomainResult<TValue> Success(TValue value)
    {
        DomainResult<TValue> domainResult = new(true)
        {
            _value = value
        };
        return domainResult;
    }

    public static DomainResult<TValue> Failure(List<string> errors)
    {
        DomainResult<TValue> domainResult = new(false)
        {
            Errors = errors
        };

        return domainResult;
    }
}

public static class DomainResult
{
    public static DomainResult<T> Success<T>(T value) => DomainResult<T>.Success(value);
    public static DomainResult<T> Failure<T>(List<string> errors) => DomainResult<T>.Failure(errors);
}