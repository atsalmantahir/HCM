using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Application.LoanApprovals.Commands.Create;
using HumanResourceManagement.Application.LoanApprovals.Queries.Get;
using HumanResourceManagement.Application.LoanGuarantors.Commands.Create;
using HumanResourceManagement.Application.LoanGuarantors.Queries.Get;
using HumanResourceManagement.Domain.Enums;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeLoans.Queries.Get;

public class EmployeeLoanVM
{
    public int Id { get; set; }
    public EmployeeProfileVM EmployeeProfile { get; set; }
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
    public List<LoanGuarantorVM> LoanGuarantors { get; set; }
    public List<LoanApprovalVM> LoanApprovals { get; set; }
}
