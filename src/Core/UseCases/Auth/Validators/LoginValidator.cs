using Core.Contracts.DTOs.Auth.Request;
using Core.Utilities.Validations;
using FluentValidation;

namespace Core.UseCases.Auth.Validators;
public class LoginValidator : AbstractValidator<LoginRequestDto>
{
	public LoginValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
				.WithMessage(ValidationMessages.NOT_EMPTY)
			.EmailAddress()
				.WithMessage(ValidationMessages.INVALID_EMAIL);

		RuleFor(x => x.Password)
			.NotEmpty()
				.WithMessage(ValidationMessages.NOT_EMPTY);
    }
}