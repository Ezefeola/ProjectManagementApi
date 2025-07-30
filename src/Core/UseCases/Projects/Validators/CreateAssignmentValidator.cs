using Core.Contracts.DTOs.Projects.Request;
using Core.Domain.Abstractions;
using Core.Domain.Projects.Entities;
using Core.Utilities.Validations;
using FluentValidation;

namespace Core.UseCases.Projects.Validators;
public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentRequestDto>
{
    public CreateAssignmentValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY);

        RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage(ValidationMessages.NOT_EMPTY)
            .MaximumLength(Assignment.Rules.TITLE_MAX_LENGTH)
                .WithMessage(DomainErrors.AssignmentErrors.TITLE_TOO_LONG);

        RuleFor(x => x.Description)
            .MaximumLength(Assignment.Rules.DESCRIPTION_MAX_LENGTH)
                .WithMessage(DomainErrors.AssignmentErrors.DESCRIPTION_TOO_LONG);

        RuleFor(x => x.Status)
            .IsInEnum()
                .WithMessage(DomainErrors.AssignmentErrors.INVALID_ASSIGNMENT_STATUS);
    }
}