using HumanResourceManagement.Domain.Enums;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.LoanApprovals.Queries.Get;

public record LoanApprovalVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ApproverName { get; set; }
    public string ApproverDesignation { get; set; }
    public DateTime ApprovalDate { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LoanApprovalStatus LoanApprovalStatus { get; set; }
    public string Reason { get; set; }
    public string Comments { get; set; }
}
