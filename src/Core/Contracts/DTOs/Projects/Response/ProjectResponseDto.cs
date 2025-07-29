using Core.Domain.Projects.ValueObjects;

namespace Core.Contracts.DTOs.Projects.Response;
public sealed record ProjectResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus.ProjectStatusEnum Status { get; set; }
}