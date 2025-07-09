using Core.Domain.Abstractions;
using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Users.Entities;
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
        public const string RoleId = "RoleId";
    }
    public static class Rules
    {
        public const int FIRSTNAME_MAX_LENGTH = 100;
        public const int FIRSTNAME_MIN_LENGTH = 1;

        public const int LASTNAME_MAX_LENGTH = 100;
        public const int LASTNAME_MIN_LENGTH = 1;

        public const int EMAIL_MAX_LENGTH = 255;
        public const int EMAIL_MIN_LENGTH = 1;

        public const int OTP_HASH_MAX_LENGTH = 6;
        public const int OTP_MAX_LENGTH = 6;
    }

    public FullName FullName { get; private set; } = default!;
    public EmailAddress EmailAddress { get; private set; } = default!;
    public string Password { get; set; } = default!;

    public RoleId RoleId { get; private set; } = default!;
    public Role? Role { get; private set; }

    private User() { }

    public static DomainResult<User> Create(
        string firstName,
        string lastName,
        string email,
        string otpHash,
        Guid roleId,
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

        DomainResult<RoleId> roleIdResult = RoleId.Create(roleId);
        if (!roleIdResult.IsSuccess)
        {
            return DomainResult<User>.Failure()
                                     .WithErrors(roleIdResult.Errors);
        }

        User user = new()
        {
            Id = UserId.NewId(),
            EmailAddress = emailAddressResult.Value,
            FullName = fullNameResult.Value,
            RoleId = roleIdResult.Value
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
       Guid? roleId
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

        DomainResult<User> roleIdResult = UpdateRoleId(roleId);
        if (!roleIdResult.IsSuccess) errors.AddRange(roleIdResult.Errors);
        totalUpdatedCount += roleIdResult.UpdatedFieldCount;

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

    public DomainResult<User> UpdateRoleId(Guid? newValue)
    {
        DomainResult<RoleId> result = RoleId.UpdateIfChanged(RoleId, newValue);

        if (!result.IsSuccess)
            return DomainResult<User>.Failure().WithErrors(result.Errors);

        RoleId = result.Value;
        return DomainResult<User>.Success()
                                 .WithValue(this)
                                 .WithUpdatedFieldCount(result.UpdatedFieldCount);
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}