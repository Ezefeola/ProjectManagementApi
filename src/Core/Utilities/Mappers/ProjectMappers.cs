using Core.Contracts.DTOs.Projects.Response;
using Core.Domain.Projects;

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
            Status = project.Status.Value
        };
    }

    public static CreateProjectResponseDto ToCreateProjectResponseDto(this Project project)
    {
        return new CreateProjectResponseDto
        {
            ProjectResponseDto = project.ToProjectResponseDto()
        };
    }
}