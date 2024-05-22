using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeReviewFromManager : BaseAuditableEntity
{
    [Column("EmployeeReviewFromManagerID")]
    public int EmployeeReviewFromManagerID { get; set; }
    public string ExternalIdentifier { get; set; }
    public DateTime SubmitBefore { get; set; }
    public bool IsSubmitted { get; set; }

    public string AdditionalCommentsFromEmployeer { get; set; }

    public string AdditionalCommentsFromManager { get; set; }

    // Foreign key
    [ForeignKey(nameof(EmployeeProfile))]
    public int ManagerId { get; set; }
    public EmployeeProfile Manager { get; set; }

    public ICollection<EmployeeReviewFromManagerWithQuestionAndAnswer> EmployeeReviewFromManagerWithQuestionAndAnswers { get; set; }
}
