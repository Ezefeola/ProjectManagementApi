namespace Core.Contracts.DTOs.Assignment.Request;
public sealed record DeleteAssignmentRequestDto
{
    public Guid ProjectId { get; set; }
    public Guid AssignmentId { get; set; }
}