using Core.Contracts.DTOs.Users.Request;
using Core.Domain.Users;
using Core.Utilities.Validations;
using FluentValidation;

namespace Core.UseCases.Users.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY)
            .MinimumLength(User.Rules.FIRST_NAME_MIN_LENGTH)
                .WithMessage(ValidationMessages.MIN_LENGTH)
            .MaximumLength(User.Rules.FIRST_NAME_MAX_LENGTH)
                .WithMessage(ValidationMessages.MAX_LENGTH);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY)
            .MinimumLength(User.Rules.LAST_NAME_MIN_LENGTH)
                .WithMessage(ValidationMessages.MIN_LENGTH)
            .MaximumLength(User.Rules.LAST_NAME_MAX_LENGTH)
                .WithMessage(ValidationMessages.MAX_LENGTH);

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY)
            .MinimumLength(User.Rules.EMAIL_MIN_LENGTH)
                .WithMessage(ValidationMessages.MIN_LENGTH)
            .MaximumLength(User.Rules.EMAIL_MAX_LENGTH)
                .WithMessage(ValidationMessages.MAX_LENGTH);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY)
            .MinimumLength(User.Rules.PASSWORD_MIN_LENGTH)
                .WithMessage(ValidationMessages.MIN_LENGTH)
            .MaximumLength(User.Rules.PASSWORD_MAX_LENGTH)
                .WithMessage(ValidationMessages.MAX_LENGTH);

        RuleFor(x => x.Role)
            .IsInEnum()
                .WithMessage(ValidationMessages.User.INVALID_ROLE)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY); 
    }
}