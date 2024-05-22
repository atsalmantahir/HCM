using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Payrolls.Commands.Create;

public record CreatePayrollCommand : IRequest<CreatePayrollCommand>
{
    public int EmployeeProfileId { get; set; }
    public DateTime PayrollDate { get; set; }
    public decimal HoursWorked { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal OvertimeRate { get; set; }
    public decimal OvertimePay { get; set; }
    public decimal HolidayHours { get; set; }
    public decimal HolidayRate { get; set; }
    public decimal HolidayPay { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetSalary { get; set; }
    public bool HasHealthInsurance { get; set; }
    public bool HasRetirementPlan { get; set; }
}

public class CreatePayrollCommandHandler : IRequestHandler<CreatePayrollCommand, CreatePayrollCommand>
{
    private readonly IPayrollsRepository repository;

    public CreatePayrollCommandHandler(IPayrollsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreatePayrollCommand> Handle(CreatePayrollCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}
