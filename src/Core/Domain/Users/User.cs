using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Users;
public sealed class User : Entity<UserId>
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
        public const int FIRST_NAME_MAX_LENGTH = 100;
        public const int FIRST_NAME_MIN_LENGTH = 1;

        public const int LAST_NAME_MAX_LENGTH = 100;
        public const int LAST_NAME_MIN_LENGTH = 1;

        public const int EMAIL_MAX_LENGTH = 255;
        public const int EMAIL_MIN_LENGTH = 2;

        public const int PASSWORD_MAX_LENGTH = 250;
        public const int PASSWORD_MIN_LENGTH = 8;
    }
    public static class AuthorizationPolicies
    {
        public const string RequireAdmin = "RequireAdmin";
        public const string RequireManager = "RequireManager";
        public const string RequireCollaborator = "RequireCollaborator";
    }
    private User() { }

    public FullName FullName { get; private set; } = default!;
    public EmailAddress EmailAddress { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public UserRole UserRole { get; private set; } = default!;
    public List<ProjectUser> ProjectUsers { get; set; } = [];
    public List<Assignment> Assignments { get; set; } = [];

    public static DomainResult<User> Create(
        string firstName,
        string lastName,
        string email,
        string password,
        UserRole.UserRolesEnum userRole,
        DateTime? createdAt = default!
    )
    {
        List<string> errors = [];

        DomainResult<FullName> fullNameResult = FullName.Create(firstName, lastName);
        if (!fullNameResult.IsSuccess) errors.AddRange(fullNameResult.Errors);

        DomainResult<EmailAddress> emailAddressResult = EmailAddress.Create(email);
        if (!emailAddressResult.IsSuccess) errors.AddRange(emailAddressResult.Errors);

        DomainResult<UserRole> userRoleResult = UserRole.Create(userRole);
        if (!userRoleResult.IsSuccess) errors.AddRange(userRoleResult.Errors);

        if(errors.Count > 0)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(errors);
        }

        User user = new()
        {
            Id = UserId.NewId(),
            EmailAddress = emailAddressResult.Value,
            FullName = fullNameResult.Value,
            UserRole = userRoleResult.Value,
        };

        if (createdAt.HasValue)
        {
            user.CreatedAt = createdAt.Value;
        }

        return DomainResult<User>.Success()
                                 .WithDescription("User created successfully.")
                                 .WithValue(user);
    }

    public DomainResult<User> SetPasswordHash(string passwordHash)
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(passwordHash)) errors.Add(DomainErrors.UserErrors.PASSWORD_NOT_EMPTY);

        if(passwordHash.Length > Rules.PASSWORD_MAX_LENGTH) errors.Add(DomainErrors.UserErrors.PASSWORD_TOO_LONG);

        if(errors.Count > 0)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(errors);
        }

        Password = passwordHash;
        return DomainResult<User>.Success()
                                 .WithValue(this);

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