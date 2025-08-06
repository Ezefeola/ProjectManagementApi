using Core.Domain.Abstractions;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Projects.ValueObjects;

namespace Core.Domain.Projects.Entities;
public sealed class ProjectUserRole : Entity<ProjectUserRoleId>
{
    public static class Rules
    {
        public const int NAME_MAX_LENGTH = 100;
    }

        private ProjectUserRole() { }

    public string Name { get; private set; } = default!;

    public static DomainResult<ProjectUserRole> Create(string name)
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(name)) errors.Add(DomainErrors.ProjectUserRoleErrors.NAME_NOT_EMPTY);
        if (name.Length > Rules.NAME_MAX_LENGTH) errors.Add(DomainErrors.ProjectUserRoleErrors.NAME_TOO_LONG);

        if(errors.Count > 0)
        {
            return DomainResult<ProjectUserRole>.Failure(errors);
        }

        ProjectUserRole projectUserRole = new()
        {
            Name = name
        };
        return DomainResult<ProjectUserRole>.Success(projectUserRole);
    }
}