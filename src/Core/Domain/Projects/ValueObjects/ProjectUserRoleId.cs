using Core.Domain.Common.StronglyTypedIds;

namespace Core.Domain.Projects.ValueObjects;
public sealed record ProjectUserRoleId : StronglyTypedGuidId<ProjectUserRoleId>
{
    public ProjectUserRoleId() { }
}