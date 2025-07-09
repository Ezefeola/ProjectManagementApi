public sealed class DomainResult<TValue>
{

    public bool IsSuccess { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public List<string> Errors { get; private set; } = [];

    private TValue? _value;
    public TValue Value => IsSuccess && _value is not null
       ? _value
       : throw new InvalidOperationException("Cannot access Value when result is failure or value is null.");

    public int UpdatedFieldCount { get; private set; }

    private DomainResult(bool isSuccess, TValue? value = default)
    {
        IsSuccess = isSuccess;
        _value = value;
    }

    public DomainResult<TValue> WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public DomainResult<TValue> WithErrors(List<string> errors)
    {
        Errors = errors.ToList();
        return this;
    }

    public DomainResult<TValue> WithUpdatedFieldCount(int count)
    {
        UpdatedFieldCount = count;
        return this;
    }


    public DomainResult<TValue> WithValue(TValue value)
    {
        _value = value;
        return this;
    }

    public static DomainResult<TValue> Success()
    {
        return new DomainResult<TValue>(true);
    }

    public static DomainResult<TValue> Failure()
    {
        return new DomainResult<TValue>(false);
    }
}

public static class DomainResult
{
    public static DomainResult<T> Success<T>() => DomainResult<T>.Success();
    public static DomainResult<T> Failure<T>() => DomainResult<T>.Failure();
}