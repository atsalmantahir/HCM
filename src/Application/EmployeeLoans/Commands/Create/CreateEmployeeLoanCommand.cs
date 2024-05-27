using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.LoanApprovals.Commands.Create;
using HumanResourceManagement.Application.LoanGuarantors.Commands.Create;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Repositories;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeLoans.Commands.Create;

public record CreateEmployeeLoanCommand : IRequest<CreateEmployeeLoanCommand>
{
    public EntityIdentifier EmployeeProfile { get; set; }
    public decimal LoanAmount { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LoanType LoanType { get; set; }
    public DateTime PaybackStartDate { get; set; }
    public DateTime? PaybackEndDate { get; set; }
    public string PaybackInterval { get; set; } // Monthly, One-time, etc.

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime? DisbursementDate { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentStatus RepaymentStatus { get; set; }
    public List<CreateLoanGuarantorCommand> LoanGuarantors { get; set; }
    public List<CreateLoanApprovalCommand> LoanApprovals { get; set; }
}

public class CreateEmployeeLoanCommandHandler : IRequestHandler<CreateEmployeeLoanCommand, CreateEmployeeLoanCommand>
{
    private readonly IEmployeeLoansRepository employeeLoansRepository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;


    public CreateEmployeeLoanCommandHandler(
        IEmployeeLoansRepository employeeLoansRepository,
        IEmployeeProfilesRepository employeeProfilesRepository)
    {
        this.employeeLoansRepository = employeeLoansRepository;
        this.employeeProfilesRepository = employeeProfilesRepository;
    }

    public async Task<CreateEmployeeLoanCommand> Handle(CreateEmployeeLoanCommand request, CancellationToken cancellationToken)
    {
        var employeeProfile = await this.employeeProfilesRepository
            .GetAsync(request.EmployeeProfile.Id);

        var entity = request.ToEmployeeLoanEntity(employeeProfile.EmployeeProfileId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await employeeLoansRepository.InsertAsync(entity, cancellationToken);

        return request;
    }
}