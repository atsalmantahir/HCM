namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeProfilesRepository
{
    IQueryable<EmployeeProfile> GetAll();
    Task<IList<EmployeeProfile>> GetAllByDesignationIdAsync(int organisationId);
    Task<EmployeeProfile> GetAsync(int id);
    Task<EmployeeProfile> GetAsync(string externalIdentifier);
    Task InsertAsync(EmployeeProfile entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeProfile entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeProfile entity, CancellationToken cancellationToken);
}
