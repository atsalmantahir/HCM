namespace HumanResourceManagement.Application.TaxSlabs.Queries.Get;

public class TaxSlabVM
{
    public string ExternalIdentifier { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTill { get; set; }
    public decimal MinimumIncome { get; set; }
    public decimal MaximumIncome { get; set; }
    public decimal BaseTax { get; set; }
    public decimal PercentageTax { get; set; }
    public decimal ExcessAmount { get; set; }
}
