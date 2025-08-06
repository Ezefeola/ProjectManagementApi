using Core.Contracts.DTOs.Common;

namespace Core.Contracts.DTOs.Projects.Response;
public sealed record GetProjectsForUserResponseDto : PaginatedResponseDto<ProjectResponseDto>
{
}