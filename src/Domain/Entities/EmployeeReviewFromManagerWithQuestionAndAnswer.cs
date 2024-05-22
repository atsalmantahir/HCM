using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeReviewFromManagerWithQuestionAndAnswer : BaseAuditableEntity
{
    [Column("EmployeeReviewFromManagerWithQuestionAndAnswerId")]

    public int EmployeeReviewFromManagerWithQuestionAndAnswerId { get; set; }
    public string ExternalIdentifier { get; set; }

    public string AdditionalComments { get; set; }
    public string AnswerGiven { get; set; }

    [ForeignKey(nameof(EmployeeReviewFromManager))]
    public int EmployeeReviewFromManagerID { get; set; }
    public EmployeeReviewFromManager EmployeeReviewFromManager { get; set; }

    [ForeignKey(nameof(ReviewQuestion))]
    public int ReviewQuestionId { get; set; }
    public ReviewQuestion ReviewQuestion { get; set; }

}
