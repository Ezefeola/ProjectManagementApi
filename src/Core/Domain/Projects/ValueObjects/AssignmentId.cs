using Core.Domain.Abstractions.StronglyTypedIds;

namespace Core.Domain.Projects.ValueObjects;
public sealed record AssignmentId : StronglyTypedGuidId<AssignmentId>
{
}
