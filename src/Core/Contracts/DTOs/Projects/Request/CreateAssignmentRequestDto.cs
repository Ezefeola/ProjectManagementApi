using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.DTOs.Projects.Request;
public sealed record CreateAssignmentRequestDto
{
    public Guid ProjectId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal? EstimatedHours { get; set; }
    public AssignmentStatus.AssignmentStatusEnum Status { get; set; } = default!;
    public Guid? UserId { get; set; }
}