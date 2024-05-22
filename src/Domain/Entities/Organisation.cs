using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class Organisation : BaseAuditableEntity
{
    [Column("OrganisationId")]

    public int OrganisationId { get; set; }
    public string ExternalIdentifier { get; set; }
    public string OrganisationName { get; set; }

    public string Logo { get; set; }

    public string Address { get; set; }

    public TimeOnly TimeIn { get; set; }

    public TimeOnly TimeOut { get; set; }

    public decimal DailyWorkingHours { get; set; }

    public IList<string> WeekendHolidays { get; set; }

    public bool IsActive { get; set; }

    public IList<Department> Departments { get; set; }
}
