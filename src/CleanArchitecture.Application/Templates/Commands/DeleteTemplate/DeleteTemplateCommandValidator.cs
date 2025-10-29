using FluentValidation;

namespace CleanArchitecture.Application.Templates.Commands.DeleteTemplate;

public class DeleteTemplateCommandValidator : AbstractValidator<DeleteTemplateCommand>
{
    public DeleteTemplateCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Template ID must be greater than 0.");
    }
}
