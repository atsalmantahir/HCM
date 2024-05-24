namespace HumanResourceManagement.Application.Allowances.Queries.Get;

public class AllowanceVM
{
    public string ExternalIdentifier { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public bool IsTaxable { get;set; }

}
