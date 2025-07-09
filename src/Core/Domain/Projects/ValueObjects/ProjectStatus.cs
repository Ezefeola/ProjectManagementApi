using Core.Contracts.Models;

namespace Core.Domain.Projects.ValueObjects;
public sealed record ProjectStatus : ValueObject
{
    public enum ProjectStatusEnum
    {
        Planned,
        InProgress,
        Completed,
        Cancelled
    }

    public ProjectStatusEnum Value { get; private set; }

    public static DomainResult<ProjectStatus> Create(ProjectStatusEnum status)
    {
        if (!Enum.IsDefined(typeof(ProjectStatusEnum), status))
        {
            return DomainResult<ProjectStatus>.Failure()
                                              .WithErrors(["Invalid Project Status"]);
        }

        ProjectStatus projectStatus = new() 
        { 
            Value = status 
        };
        return DomainResult<ProjectStatus>.Success()
                                          .WithValue(projectStatus);
    }
}