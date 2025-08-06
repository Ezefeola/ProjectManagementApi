using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.DTOs.Assignment.Response;
using Core.Domain.Projects;
using Core.Domain.Projects.Entities;
using Core.Utilities.QueryOptions.Pagination;

namespace Core.Utilities.Mappers;
public static class AssignmentMappers
{
    public static AssignmentResponseDto ToAssignmentResponseDto(this Assignment assignment)
    {
        return new AssignmentResponseDto()
        {
            Id = assignment.Id.Value,
            Title = assignment.Title,
            Description = assignment.Description,
            EstimatedHours = assignment.EstimatedHours,
            Status = assignment.Status.Value,
            LoggedHours = assignment.LoggedHours,
            UserIds = [.. assignment.AssignmentUsers.Select(x => x.UserId.Value)]
        };
    }

    public static GetAssignmentsForProjectResponseDto ToGetAssignmentsForProjectResponseDto(
        this IEnumerable<Assignment> assignments,
        GetAssignmentsForProjectRequestDto parametersRequestDto,
        int totalAssignmentsForProject
    )
    {
        return new GetAssignmentsForProjectResponseDto()
        {
            PageIndex = parametersRequestDto.GetPageIndex(),
            PageSize = parametersRequestDto.GetPageSize(),
            TotalPages = parametersRequestDto.GetTotalPages(totalAssignmentsForProject),
            TotalRecords = totalAssignmentsForProject,
            Items = [.. assignments.Select(assignment => assignment.ToAssignmentResponseDto())]
        };
    }

    public static CreateAssignmentResponseDto ToCreateAssignmentResponseDto(this Project project)
    {
        return new CreateAssignmentResponseDto()
        {
            ProjectResponseDto = project.ToProjectResponseDto()
        };
    }
}