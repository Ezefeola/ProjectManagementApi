using Core.Contracts.Models;

namespace Core.Domain.Users.ValueObjects;
public sealed record RoleId : StronglyTypedId<Guid>
{
    private RoleId() : base(Guid.Empty) { }
    private RoleId(Guid value) : base(value) { }

    public static RoleId NewEfId(Guid value) => new(value);
    public static DomainResult<RoleId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return DomainResult<RoleId>.Failure()
                                       .WithErrors(["Invalid RoleId"]);
        }


        RoleId userId = new(value);
        return DomainResult<RoleId>.Success()
                                   .WithValue(userId);
    }

    public static DomainResult<RoleId> UpdateIfChanged(RoleId current, Guid? newValue)
    {
        if (newValue is null || newValue == current.Value)
        {
            return DomainResult<RoleId>.Success()
                                       .WithValue(current)
                                       .WithDescription("No changes were made to RoleId.");
        }

        DomainResult<RoleId> roleIdResult = Create(newValue.Value);
        if (!roleIdResult.IsSuccess) return roleIdResult;

        return roleIdResult.WithDescription("RoleId updated.")
                           .WithUpdatedFieldCount(1);
    }
}