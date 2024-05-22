using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Common.Models;

public class EntityExternalIdentifier
{
    [Required]
    public string ExternalIdentifier { get; set; }
}
