using Core.Contracts.DTOs.Projects.Request;
using Core.Domain.Common;
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

        RuleFor(x => x.StartDate)
           .LessThan(x => x.EndDate)
               .WithMessage(DomainErrors.ProjectErrors.START_DATE_BEFORE_END_DATE);

        RuleFor(x => x.EndDate)
           .GreaterThan(x => x.StartDate)
               .WithMessage(DomainErrors.ProjectErrors.START_DATE_BEFORE_END_DATE);

        RuleFor(x => x.Status)
           .IsInEnum()
              .WithMessage(DomainErrors.ProjectErrors.INVALID_STATUS);
    }
}