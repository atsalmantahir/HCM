namespace HumanResourceManagement.Domain.Repositories;

public interface IDepartmentsRepository
{
    IQueryable<Department> GetAll(string organisationExternalIdentifier);
    Task<Department> GetAsync(int id);
    Task<Department> GetAsync(string organisationExternalIdentifier, string externalIdentifier);
    Task<Department> GetAsync(string externalIdentifier);
    Task InsertAsync(Department entity, CancellationToken cancellationToken);
    Task UpdateAsync(Department entity, CancellationToken cancellationToken);
    Task DeleteAsync(Department entity, CancellationToken cancellationToken);
}
