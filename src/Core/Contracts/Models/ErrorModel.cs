namespace Core.Contracts.Models;
public class ErrorModel
{
    public bool IsSuccess { get; set; }
    public List<string> ErrorMessages { get; set; } = [];
}