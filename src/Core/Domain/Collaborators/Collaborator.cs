using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Collaborators.ValueObjects;

namespace Core.Domain.Collaborators;
public sealed class Collaborator : Entity<CollaboratorId>
{
    private Collaborator() { }

    public FullName FullName { get; private set; } = default!;
    public EmailAddress Email { get; private set; } = default!;
    public int MyProperty { get; set; } 
}
