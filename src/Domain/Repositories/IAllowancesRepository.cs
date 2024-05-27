namespace HumanResourceManagement.Domain.Repositories;

public interface IAllowancesRepository
{
    IQueryable<Allowance> GetAll();
    Task<Allowance> GetAsync(int id);
    Task InsertAsync(Allowance entity, CancellationToken cancellationToken);
    Task UpdateAsync(Allowance entity, CancellationToken cancellationToken);
    Task DeleteAsync(Allowance entity, CancellationToken cancellationToken);
}
