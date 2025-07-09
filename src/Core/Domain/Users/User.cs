using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Users;
public class User : AggregateRoot<UserId>
{
    public static class ColumnNames
    {
        public const string Id = "Id";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Email = "Email";
        public const string Password = "Password";
        public const string UserRole = "UserRole";
    }
    public static class Rules
    {
        public const int FIRSTNAME_MAX_LENGTH = 100;
        public const int FIRSTNAME_MIN_LENGTH = 1;

        public const int LASTNAME_MAX_LENGTH = 100;
        public const int LASTNAME_MIN_LENGTH = 1;

        public const int EMAIL_MAX_LENGTH = 255;
        public const int EMAIL_MIN_LENGTH = 1;

        public const int PASSWORD_MAX_LENGTH = 250;
    }

    private User() { }

    public FullName FullName { get; private set; } = default!;
    public EmailAddress EmailAddress { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public UserRole UserRole { get; private set; } = default!;

    public static DomainResult<User> Create(
        string firstName,
        string lastName,
        string email,
        string password,
        UserRole.UserRolesEnum userRole,
        DateTime? createdAt = default!
    )
    {
        DomainResult<FullName> fullNameResult = FullName.Create(firstName, lastName);
        if (!fullNameResult.IsSuccess)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(fullNameResult.Errors);
        }

        DomainResult<EmailAddress> emailAddressResult = EmailAddress.Create(email);
        if (!emailAddressResult.IsSuccess)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(emailAddressResult.Errors);
        }

        DomainResult<UserRole> userRoleResult = UserRole.Create(userRole);
        if (!userRoleResult.IsSuccess)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(userRoleResult.Errors);
        }

        User user = new()
        {
            Id = UserId.NewId(),
            EmailAddress = emailAddressResult.Value,
            FullName = fullNameResult.Value,
            UserRole = userRoleResult.Value
        };

        if (createdAt.HasValue)
        {
            user.CreatedAt = createdAt.Value;
        }

        return DomainResult<User>.Success()
                                 .WithDescription("User created successfully.")
                                 .WithValue(user);
    }

    public DomainResult<User> Update(
       string? firstName,
       string? lastName,
       string? email,
       UserRole.UserRolesEnum? userRole
   )
    {
        List<string> errors = [];
        int totalUpdatedCount = 0;

        DomainResult<User> fullNameResult = UpdateFullName(firstName, lastName);
        if (!fullNameResult.IsSuccess) errors.AddRange(fullNameResult.Errors);
        totalUpdatedCount += fullNameResult.UpdatedFieldCount;

        DomainResult<User> emailResult = UpdateEmail(email);
        if (!emailResult.IsSuccess) errors.AddRange(emailResult.Errors);
        totalUpdatedCount += emailResult.UpdatedFieldCount;

        DomainResult<User> userRoleResult = UpdateUserRole(userRole);
        if (!userRoleResult.IsSuccess) errors.AddRange(userRoleResult.Errors);
        totalUpdatedCount += userRoleResult.UpdatedFieldCount;

        if (errors.Count > 0)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(errors);
        }

        string descriptionMessage = totalUpdatedCount > 0
                                    ? $"User updated successfully. {totalUpdatedCount}"
                                    : "No changes were made.";

        return DomainResult<User>.Success()
                                 .WithValue(this)
                                 .WithDescription(descriptionMessage);
    }

    public DomainResult<User> UpdateFullName(string? firstName, string? lastName)
    {
        DomainResult<FullName> result = FullName.UpdateIfChanged(firstName, lastName);
        if (!result.IsSuccess)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(result.Errors);
        }

        FullName = result.Value;
        return DomainResult<User>.Success()
                                 .WithValue(this)
                                 .WithUpdatedFieldCount(result.UpdatedFieldCount);
    }

    public DomainResult<User> UpdateEmail(string? email)
    {
        DomainResult<EmailAddress> result = EmailAddress.UpdateIfChanged(email);
        if (!result.IsSuccess)
            return DomainResult<User>.Failure().WithErrors(result.Errors);

        EmailAddress = result.Value;
        return DomainResult<User>.Success()
                                 .WithValue(this)
                                 .WithUpdatedFieldCount(result.UpdatedFieldCount);
    }

    public DomainResult<User> UpdateUserRole(UserRole.UserRolesEnum? userRole)
    {
        DomainResult<UserRole> userRoleResult = UserRole.UpdateIfChanged(userRole);
        if (!userRoleResult.IsSuccess)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(userRoleResult.Errors);
        }

        return DomainResult<User>.Success()
                                 .WithValue(this)
                                 .WithUpdatedFieldCount(userRoleResult.UpdatedFieldCount);
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}