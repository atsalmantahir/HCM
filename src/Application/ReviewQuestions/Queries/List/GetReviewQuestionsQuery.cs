using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.ReviewQuestions.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.ReviewQuestions.Queries.List;

public record GetReviewQuestionsQuery : PaginatedQuery, IRequest<PaginatedList<ReviewQuestionVM>>;

public class GetReviewQuestionsQueryHandler : IRequestHandler<GetReviewQuestionsQuery, PaginatedList<ReviewQuestionVM>>
{
    private readonly IReviewQuestionsRepository reviewQuestionsRepository;

    public GetReviewQuestionsQueryHandler(IReviewQuestionsRepository reviewQuestionsRepository)
    {
        this.reviewQuestionsRepository = reviewQuestionsRepository;
    }

    public async Task<PaginatedList<ReviewQuestionVM>> Handle(GetReviewQuestionsQuery request, CancellationToken cancellationToken)
    {
        var reviewQuestions = this.reviewQuestionsRepository.GetAll();
        var response = reviewQuestions.ToDto();
        return await PaginatedList<ReviewQuestionVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}