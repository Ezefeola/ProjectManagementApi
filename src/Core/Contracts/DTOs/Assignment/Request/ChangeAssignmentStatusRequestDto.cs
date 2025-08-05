using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.DTOs.Assignment.Request;
public sealed record ChangeAssignmentStatusRequestDto
{
    public AssignmentStatus.AssignmentStatusEnum Status { get; set; }
}