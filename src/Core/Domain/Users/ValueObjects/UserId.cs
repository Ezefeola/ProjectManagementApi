using Core.Domain.Common.StronglyTypedIds;

namespace Core.Domain.Users.ValueObjects;
public sealed record UserId : StronglyTypedGuidId<UserId>
{
    public UserId()
    {
        
    }
}