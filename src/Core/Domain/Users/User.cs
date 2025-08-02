using Core.Domain.Abstractions;
using Core.Domain.Common;
using Core.Domain.Common.DomainResults;
using Core.Domain.Common.ValueObjects;
using Core.Domain.Projects.Entities;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Users;
public sealed class User : Entity<UserId>
{
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
    public UserRole Role { get; private set; } = default!;
    public List<ProjectUser> ProjectUsers { get; set; } = [];
    public List<Assignment> Assignments { get; set; } = [];

    public static DomainResult<User> Create(
        string firstName,
        string lastName,
        string email,
        string password,
        UserRole.UserRolesEnum userRole
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
            return DomainResult<User>.Failure(errors);
        }

        User user = new()
        {
            Id = UserId.NewId(),
            EmailAddress = emailAddressResult.Value,
            FullName = fullNameResult.Value,
            Role = userRoleResult.Value,
        };

        return DomainResult<User>.Success(user)
                                 .WithDescription("User created successfully.");
    }

    public DomainResult<User> SetPasswordHash(string passwordHash)
    {
        List<string> errors = [];

        if (string.IsNullOrWhiteSpace(passwordHash)) errors.Add(DomainErrors.UserErrors.PASSWORD_NOT_EMPTY);

        if(passwordHash.Length > Rules.PASSWORD_MAX_LENGTH) errors.Add(DomainErrors.UserErrors.PASSWORD_TOO_LONG);

        if(errors.Count > 0)
        {
            return DomainResult<User>.Failure(errors);
        }

        Password = passwordHash;
        return DomainResult<User>.Success(this);

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
            return DomainResult<User>.Failure(errors);
        }

        string descriptionMessage = totalUpdatedCount > 0
                                    ? $"User updated successfully. {totalUpdatedCount}"
                                    : "No changes were made.";

        return DomainResult<User>.Success(this)
                                 .WithDescription(descriptionMessage);
    }

    public DomainResult<User> UpdateFullName(string? firstName, string? lastName)
    {
        DomainResult<FullName> fullNameResult = FullName.UpdateIfChanged(firstName, lastName);
        if (!fullNameResult.IsSuccess)
        {
            return DomainResult<User>.Failure(fullNameResult.Errors);
        }

        FullName = fullNameResult.Value;
        return DomainResult<User>.Success(this)
                                 .WithUpdatedFieldCount(fullNameResult.UpdatedFieldCount);
    }

    public DomainResult<User> UpdateEmail(string? email)
    {
        DomainResult<EmailAddress> emailAddressResult = EmailAddress.UpdateIfChanged(email);
        if (!emailAddressResult.IsSuccess)
            return DomainResult<User>.Failure(emailAddressResult.Errors);

        EmailAddress = emailAddressResult.Value;
        return DomainResult<User>.Success(this)
                                 .WithUpdatedFieldCount(emailAddressResult.UpdatedFieldCount);
    }

    public DomainResult<User> UpdateUserRole(UserRole.UserRolesEnum? userRole)
    {
        DomainResult<UserRole> userRoleResult = Role.UpdateIfChanged(userRole);
        if (!userRoleResult.IsSuccess)
        {
            return DomainResult<User>.Failure(userRoleResult.Errors);
        }

        return DomainResult<User>.Success(this)
                                 .WithUpdatedFieldCount(userRoleResult.UpdatedFieldCount);
    }
}