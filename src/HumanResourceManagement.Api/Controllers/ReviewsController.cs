using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HumanResourceManagement.Application.Payrolls.Queries.List;
using HumanResourceManagement.Application.Payrolls.Commands.Create;
using HumanResourceManagement.Application.ReviewQuestions.Queries.List;
using HumanResourceManagement.Application.ReviewQuestions.Commands.Create;

namespace HumanResourceManagement.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IMediator mediator;

    public ReviewsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Route("questions")]
    public async Task<IResult> ListReviewQuestionsAsync([FromQuery] GetReviewQuestionsQuery query)
    {
        var response = await mediator.Send(query).ConfigureAwait(false);
        return Results.Ok(response);
    }

    [HttpPost]
    [Route("questions")]
    public async Task<IResult> CreateReviewQuestionAsync(
        [FromBody] CreateReviewQuestionCommand createPayrollStatusCommand)
    {
        if (!ModelState.IsValid) 
        {
            return Results.BadRequest(ModelState);
        }

        var response = await mediator.Send(createPayrollStatusCommand);
        return Results.Ok(response);
    }
}
