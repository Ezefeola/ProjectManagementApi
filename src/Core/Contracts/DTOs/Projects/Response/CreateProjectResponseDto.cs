namespace Core.Contracts.DTOs.Projects.Response;
public sealed record CreateProjectResponseDto
{
    public ProjectResponseDto ProjectResponseDto { get; set; } = default!;
}