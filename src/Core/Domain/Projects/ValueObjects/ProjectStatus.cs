using Core.Contracts.Models;
using Core.Domain.Abstractions;

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

    public bool IsPlanned() => Value == ProjectStatusEnum.Planned;
    public bool IsInProgress() => Value == ProjectStatusEnum.InProgress;
    public bool IsCompleted() => Value == ProjectStatusEnum.Completed;
    public bool IsCancelled() => Value == ProjectStatusEnum.Cancelled;

    public ProjectStatusEnum Value { get; private set; }

    public static DomainResult<ProjectStatus> Create(ProjectStatusEnum status)
    {
        if (!Enum.IsDefined(status))
        {
            return DomainResult<ProjectStatus>.Failure([DomainErrors.ProjectErrors.INVALID_STATUS]);
        }

        ProjectStatus projectStatus = new() 
        { 
            Value = status 
        };
        return DomainResult<ProjectStatus>.Success(projectStatus);
    }
}