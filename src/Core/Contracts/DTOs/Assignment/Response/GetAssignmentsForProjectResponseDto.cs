using Core.Contracts.DTOs.Common;

namespace Core.Contracts.DTOs.Assignment.Response;
public sealed record GetAssignmentsForProjectResponseDto : PaginatedResponseDto<AssignmentResponseDto>
{
}