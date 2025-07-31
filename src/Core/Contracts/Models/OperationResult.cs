namespace Core.Contracts.Models;
public record OperationResult
{
    public bool IsSuccess { get; set; }
    public IReadOnlyCollection<string> Errors { get; set; }
    public OperationResult(bool isSuccess, IReadOnlyCollection<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
    public static OperationResult Success() => new(true, Array.Empty<string>());
    public static OperationResult Failure(IEnumerable<string> errors) => new(false, errors.ToList());
}