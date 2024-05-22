using HumanResourceManagement.Application.Departments.Commands;

namespace HumanResourceManagement.Application.Designations.Commands.Create;

public class CreateDesignationCommandValidator : AbstractValidator<CreateDesignationCommand>
{
    public CreateDesignationCommandValidator()
    {
        RuleFor(v => v.DesignationName)
            .MaximumLength(200)
            .MinimumLength(5)
            .NotEmpty();

    }
}