using HumanResourceManagement.Domain.Entities;

namespace HumanResourceManagement.Domain.Repositories;

public interface IReviewQuestionsRepository
{
    IQueryable<ReviewQuestion> GetAll();
    Task<ReviewQuestion> GetAsync(int id);
    Task InsertAsync(ReviewQuestion entity, CancellationToken cancellationToken);
    Task UpdateAsync(ReviewQuestion entity, CancellationToken cancellationToken);
    Task DeleteAsync(ReviewQuestion entity, CancellationToken cancellationToken);
}
