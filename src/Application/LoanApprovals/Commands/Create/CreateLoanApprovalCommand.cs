using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Enums;

namespace HumanResourceManagement.Application.LoanApprovals.Commands.Create;

public record CreateLoanApprovalCommand : IRequest<Result<CreateLoanApprovalCommand>>
{
    public string Title { get; set; }
    public string ApproverName { get; set; }
    public string ApproverDesignation { get; set; }
    public DateTime ApprovalDate { get; set; }
    public LoanApprovalStatus LoanApprovalStatus { get; set; }
    public string Reason { get; set; }
    public string Comments { get; set; }
}
