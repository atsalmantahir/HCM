using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Common.Models;

public class EntityIdentifier
{
    [Required]
    public int Id { get; set; }
}
