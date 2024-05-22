using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Payrolls.Services.Models;

public class PayrollRequest
{
    public string OrganisationExternalIdentifier { get; set; }

    [Required]
    public int Month { get; set; }

    [Required]
    public int Year { get; set; }
    //public DateTime StartDate { get; set; }
    //public DateTime EndDate { get; set; }
}
