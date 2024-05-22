using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Holidays.Commands.Create;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.ReviewQuestions.Commands.Create;

public record CreateReviewQuestionCommand : IRequest<CreateReviewQuestionCommand>
{
    [Required]
    public string Text { get; set; }

    [Required]
    public decimal? MinValue { get; set; }

    [Required]
    public decimal? MaxValue { get; set; }
}

public class CreateReviewQuestionCommandHandler : IRequestHandler<CreateReviewQuestionCommand, CreateReviewQuestionCommand>
{
    private readonly IReviewQuestionsRepository reviewQuestionsRepository;

    public CreateReviewQuestionCommandHandler(IReviewQuestionsRepository reviewQuestionsRepository)
    {
        this.reviewQuestionsRepository = reviewQuestionsRepository;
    }

    public async Task<CreateReviewQuestionCommand> Handle(CreateReviewQuestionCommand request, CancellationToken cancellationToken)
    {
        var reviewQuestions = this.reviewQuestionsRepository.GetAll();
        if (reviewQuestions.Any(x => x.Text.ToLower() == request.Text.ToLower()))
        {
            throw new ConflictRequestException($"Question already exists for text '{request.Text}' provided");
        }

        var entity = request.ToEntity();

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await reviewQuestionsRepository.InsertAsync(entity, cancellationToken);

        return request;
    }
}
