using Core.Contracts.DTOs.Projects.Response;

namespace Core.Contracts.DTOs.Assignment.Response;
public sealed record CreateAssignmentResponseDto
{
    public ProjectResponseDto ProjectResponseDto { get; set; } = default!;
}