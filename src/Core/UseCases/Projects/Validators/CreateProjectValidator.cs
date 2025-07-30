using Core.Contracts.DTOs.Projects.Request;
using Core.Domain.Abstractions;
using Core.Domain.Projects;
using Core.Utilities.Validations;
using FluentValidation;

namespace Core.UseCases.Projects.Validators;
public class CreateProjectValidator : AbstractValidator<CreateProjectRequestDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage(DomainErrors.ProjectErrors.NAME_NOT_EMPTY)
            .MaximumLength(Project.Rules.NAME_MAX_LENGTH)
                .WithMessage(ValidationMessages.MAX_LENGTH);
    }
}