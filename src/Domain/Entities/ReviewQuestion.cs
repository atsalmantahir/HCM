using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class ReviewQuestion : BaseAuditableEntity
{
    [Column("ReviewQuestionId")]
    public int ReviewQuestionId { get; set; }

    public string ExternalIdentifier { get; set; }

    public string Text { get; set; }

    // For integer and floating point questions, define min and max values
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public bool IsActive { get; set; }
}
