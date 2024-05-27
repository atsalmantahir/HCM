using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class Allowance : BaseAuditableEntity
{
    [Column("AllowanceId")]
    public int AllowanceId { get; set; }
    public string Name { get; set; }
}
