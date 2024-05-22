namespace HumanResourceManagement.Domain.Repositories;

public interface IDesignationsRepository
{
    IQueryable<Designation> GetAll(string departmentExternalIdentifier);
    Task<Designation> GetAsync(int id);
    Task<Designation> GetAsync(string departmentExternalIdentifier, string externalIdentifier);
    Task<Designation> GetAsync(string externalIdentifier);
    Task InsertAsync(Designation entity, CancellationToken cancellationToken);
    Task UpdateAsync(Designation entity, CancellationToken cancellationToken);
    Task DeleteAsync(Designation entity, CancellationToken cancellationToken);
}
