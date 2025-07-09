using Core.Contracts.Models;
using Core.Domain.Abstractions;

namespace Core.Domain.Users.ValueObjects;
public sealed record UserId : StronglyTypedId<Guid>
{
    private UserId() : base(Guid.Empty) { }
    private UserId(Guid value) : base(value) { }

    public static UserId NewId() => new(Guid.NewGuid());
    public static UserId NewEfId(Guid value) => new(value);
    public static DomainResult<UserId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return DomainResult<UserId>.Failure()
                                       .WithErrors(["Invalid UserId"]);
        }


        UserId userId = new(value);
        return DomainResult<UserId>.Success()
                                   .WithValue(userId);
    }
}