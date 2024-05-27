namespace HumanResourceManagement.Domain.Repositories;

public interface ITaxSlabsRepository
{
    IQueryable<TaxSlab> GetAll();
    Task<TaxSlab> GetAsync(int id);
    Task InsertAsync(TaxSlab entity, CancellationToken cancellationToken);
    Task UpdateAsync(TaxSlab entity, CancellationToken cancellationToken);
    Task DeleteAsync(TaxSlab entity, CancellationToken cancellationToken);
}
