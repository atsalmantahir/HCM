namespace HumanResourceManagement.Application.Departments.Commands.Create;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(v => v.DepartmentName)
            .MaximumLength(200)
            .MinimumLength(5)
            .NotEmpty();

    }
}
