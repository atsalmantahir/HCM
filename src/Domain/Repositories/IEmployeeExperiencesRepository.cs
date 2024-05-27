namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeExperiencesRepository
{
    IQueryable<EmployeeExperience> GetAll();
    IQueryable<EmployeeExperience> GetAll(int employeeProfileId);
    Task<EmployeeExperience> GetAsync(int id);
    Task InsertAsync(EmployeeExperience entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeExperience entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeExperience entity, CancellationToken cancellationToken);
}
