using HumanResourceManagement.Domain.Enums;

namespace HumanResourceManagement.Application.LoanGuarantors.Queries.Get;

public record LoanGuarantorVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Relationship { get; set; }
    public string ContactInfo { get; set; }
}
