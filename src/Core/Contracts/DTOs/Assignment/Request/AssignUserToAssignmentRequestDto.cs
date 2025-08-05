namespace Core.Contracts.DTOs.Assignment.Request;
public sealed record AssignUserToAssignmentRequestDto
{
    public required Guid UserId { get; set; }
}