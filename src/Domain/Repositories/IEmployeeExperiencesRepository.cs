namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeExperiencesRepository
{
    IQueryable<EmployeeExperience> GetAll();
    Task<EmployeeExperience> GetAsync(int id);
    Task<EmployeeExperience> GetAsync(string externalIdentifier);
    Task InsertAsync(EmployeeExperience entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeExperience entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeExperience entity, CancellationToken cancellationToken);
}
