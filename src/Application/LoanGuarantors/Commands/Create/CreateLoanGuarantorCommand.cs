using HumanResourceManagement.Application.Common.Models;

namespace HumanResourceManagement.Application.LoanGuarantors.Commands.Create;

public record CreateLoanGuarantorCommand : IRequest<Result<CreateLoanGuarantorCommand>>
{
    public string Name { get; set; }
    public string Relationship { get; set; }
    public string ContactInfo { get; set; }
}
