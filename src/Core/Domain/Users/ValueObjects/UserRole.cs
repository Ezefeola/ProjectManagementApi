using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Common.DomainResults;

namespace Core.Domain.Users.ValueObjects;
public sealed record UserRole : ValueObject
{
    public enum UserRolesEnum
    {
        Admin = 1,
        Manager = 2,
        Collaborator = 3
    }

    private UserRole() { }

    public UserRolesEnum Value { get; private set; }

    public static DomainResult<UserRole> Create(UserRolesEnum userRoleValue)
    {
        if (!Enum.IsDefined(userRoleValue))
        {
            return DomainResult<UserRole>.Failure([$"Invalid user role: {userRoleValue}"]);
        }
        
        UserRole userRole = new()
        {
            Value = userRoleValue
        };
        return DomainResult<UserRole>.Success(userRole);
    }

    public DomainResult<UserRole> UpdateIfChanged(UserRolesEnum? userRole)
    {
        if(userRole is null)
        {
            return DomainResult<UserRole>.Success(this);
        }

        if (Value == userRole.Value)
        {
            return DomainResult<UserRole>.Success(this)
                                         .WithUpdatedFieldCount(0);
        }

        Value = userRole.Value;

        return DomainResult<UserRole>.Success(this);
    }
}
