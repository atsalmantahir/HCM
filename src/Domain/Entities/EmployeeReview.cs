using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeReview : BaseAuditableEntity
{
    [Column("EmployeeReviewID")]
    public int EmployeeReviewID { get; set; }
    public string ExternalIdentifier { get; set; }

    public DateTime SubmitBefore { get;set; }

    public bool IsSubmitted { get; set; }

    // Foreign key
    [ForeignKey(nameof(EmployeeProfile))]
    public int EmployeeProfileId { get; set; }
    public EmployeeProfile EmployeeProfile { get; set; }

    public ICollection<EmployeeReviewFromManager> EmployeeReviewFromManagers { get; set; }
}
