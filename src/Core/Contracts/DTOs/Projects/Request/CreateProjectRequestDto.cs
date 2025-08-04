using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.DTOs.Projects.Request;
public sealed record CreateProjectRequestDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required ProjectStatus.ProjectStatusEnum Status { get; set; }
}