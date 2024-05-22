using HumanResourceManagement.Application.Designations.Commands;

namespace HumanResourceManagement.Application.EmployeeProfiles.Commands.Create;

public class CreateEmployeeProfileCommandValidator : AbstractValidator<CreateEmployeeProfileCommand>
{
    public CreateEmployeeProfileCommandValidator()
    {
        RuleFor(v => v.EmployeeName)
            .MaximumLength(200)
            .MinimumLength(5)
            .NotEmpty();

    }
}