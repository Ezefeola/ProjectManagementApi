using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.DTOs.Assignment.Response;
public sealed record AssignmentResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public decimal? EstimatedHours { get; set; } = default!;
    public decimal? LoggedHours { get; set; } = default!;
    public AssignmentStatus.AssignmentStatusEnum Status { get; set; } = default!;
    public List<Guid> UserIds { get; set; } = [];
}