using Core.Contracts.DTOs.Common;

namespace Core.Contracts.DTOs.Projects.Response;
public sealed record GetProjectsResponseDto : PaginatedResponseDto<ProjectResponseDto>
{
}