using Core.Contracts.Models;

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
            return DomainResult<UserRole>.Failure()
                                         .WithErrors([$"Invalid user role: {userRoleValue}"]);
        }
        
        UserRole userRole = new()
        {
            Value = userRoleValue
        };
        return DomainResult<UserRole>.Success()
                                     .WithValue(userRole);
    }

    public DomainResult<UserRole> UpdateIfChanged(UserRolesEnum? userRole)
    {
        if(userRole is null)
        {
            return DomainResult<UserRole>.Success()
                                         .WithValue(this);
        }

        if (Value == userRole.Value)
        {
            return DomainResult<UserRole>.Success()
                                         .WithValue(this)
                                         .WithUpdatedFieldCount(0);
        }

        Value = userRole.Value;

        return DomainResult<UserRole>.Success()
                                     .WithValue(this);
    }
}
