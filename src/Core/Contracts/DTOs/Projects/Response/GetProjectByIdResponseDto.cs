namespace Core.Contracts.DTOs.Projects.Response;
public sealed record GetProjectByIdResponseDto
{
    public ProjectResponseDto ProjectResponseDto { get; set; } = default!;
}