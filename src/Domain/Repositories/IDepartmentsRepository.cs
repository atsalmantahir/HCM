namespace HumanResourceManagement.Domain.Repositories;

public interface IDepartmentsRepository
{
    IQueryable<Department> GetAll(int organisationId);
    Task<Department> GetAsync(int id);
    Task InsertAsync(Department entity, CancellationToken cancellationToken);
    Task UpdateAsync(Department entity, CancellationToken cancellationToken);
    Task DeleteAsync(Department entity, CancellationToken cancellationToken);
}
