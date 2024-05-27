namespace HumanResourceManagement.Application.ReviewQuestions.Queries.Get;

public class ReviewQuestionVM
{
    public int Id { get; set; }
    public string Text { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public bool IsActive { get; set; }
}
