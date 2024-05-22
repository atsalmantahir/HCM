namespace HumanResourceManagement.Application.ReviewQuestions.Queries.Get;

public class ReviewQuestionVM
{
    public string ExternalIdentifier { get; set; }
    public string Text { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public bool IsActive { get; set; }
}
