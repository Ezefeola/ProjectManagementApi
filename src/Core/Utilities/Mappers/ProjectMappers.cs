using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Domain.Projects;
using Core.Utilities.QueryOptions.Pagination;

namespace Core.Utilities.Mappers;
public static class ProjectMappers
{
    public static ProjectResponseDto ToProjectResponseDto(this Project project)
    {
        return new ProjectResponseDto
        {
            Id = project.Id.Value,
            Name = project.Name,
            StartDate = project.ProjectPeriod.StartDate,
            EndDate = project.ProjectPeriod.EndDate,
            Status = project.Status.Value,
            AssignmentsResponseDto = [.. project.Assignments.Select(x => x.ToAssignmentResponseDto())],
        };
    }

    public static CreateProjectResponseDto ToCreateProjectResponseDto(this Project project)
    {
        return new CreateProjectResponseDto
        {
            ProjectResponseDto = project.ToProjectResponseDto()
        };
    }

    public static CreateAssignmentResponseDto ToCreateAssignmentResponseDto(this Project project)
    {
        return new CreateAssignmentResponseDto()
        {
            ProjectResponseDto = project.ToProjectResponseDto()
        };
    }

    public static GetProjectsResponseDto ToGetProjectsResponseDto(
        this IEnumerable<Project> projects, 
        GetProjectsRequestDto parametersRequestDto,
        int projectsCount
    )
    {
        return new GetProjectsResponseDto
        {
            PageIndex = parametersRequestDto.GetPageIndex(),
            PageSize = parametersRequestDto.GetPageSize(),
            TotalPages = parametersRequestDto.GetTotalPages(projectsCount),
            TotalRecords = projectsCount,
            Items = [.. projects.Select(x => x.ToProjectResponseDto())]
        };
    }

    public static GetProjectByIdResponseDto ToGetProjectByIdResponseDto(this Project project)
    {
        return new GetProjectByIdResponseDto()
        {
            ProjectResponseDto = project.ToProjectResponseDto()
        };
    }
}