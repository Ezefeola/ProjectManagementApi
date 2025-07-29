namespace Core.Contracts.DTOs.Projects.Response;
public sealed record CreateAssignmentResponseDto
{
    public ProjectResponseDto ProjectResponseDto { get; set; } = default!;
}