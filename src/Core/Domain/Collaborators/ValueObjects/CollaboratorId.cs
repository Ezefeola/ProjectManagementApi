using Core.Domain.Abstractions.StronglyTypedIds;

namespace Core.Domain.Collaborators.ValueObjects;
public sealed record CollaboratorId : StronglyTypedGuidId<CollaboratorId>
{
    public CollaboratorId() { }
}