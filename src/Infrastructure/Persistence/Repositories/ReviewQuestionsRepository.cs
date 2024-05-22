using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Persistence.Repositories;

public class ReviewQuestionsRepository : IReviewQuestionsRepository
{
    private readonly IApplicationDbContext context;


    public ReviewQuestionsRepository(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task DeleteAsync(ReviewQuestion entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.context.ReviewQuestions.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<ReviewQuestion> GetAll()
    {
        return this.context.ReviewQuestions.Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task<ReviewQuestion> GetAsync(int id)
    {
        return await this
             .context
             .ReviewQuestions
             .AsNoTracking()
             .FirstOrDefaultAsync(x => x.ReviewQuestionId == id && x.IsDeleted == false);
    }

    public async Task<ReviewQuestion> GetAsync(string externalIdentifier)
    {
        return await this
            .context
            .ReviewQuestions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalIdentifier == externalIdentifier && x.IsDeleted == false);
    }

    public async Task InsertAsync(ReviewQuestion entity, CancellationToken cancellationToken)
    {
        await this.context.ReviewQuestions.AddAsync(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ReviewQuestion entity, CancellationToken cancellationToken)
    {
        this.context.ReviewQuestions.Update(entity);
        await this.context.SaveChangesAsync(cancellationToken);
    }
}
