namespace HumanResourceManagement.Domain.Repositories;

public interface IOrganisationsRepository
{
    IQueryable<Organisation> GetAll();
    Task<Organisation> GetAsync(int id);
    Task<Organisation> GetAsync(string externalIdentifier);
    Task InsertAsync(Organisation entity, CancellationToken cancellationToken);
    Task UpdateAsync(Organisation entity, CancellationToken cancellationToken);
    Task DeleteAsync(Organisation entity, CancellationToken cancellationToken);
}
