namespace HumanResourceManagement.Domain.Repositories;

public interface IEmployeeEducationsRepository
{
    Task<IList<EmployeeEducation>> GetAllAsync();
    Task<IList<EmployeeEducation>> GetAllAsync(int employeeProfileId);
    Task<EmployeeEducation> GetAsync(int id);
    Task InsertAsync(EmployeeEducation entity, CancellationToken cancellationToken);
    Task UpdateAsync(EmployeeEducation entity, CancellationToken cancellationToken);
    Task DeleteAsync(EmployeeEducation entity, CancellationToken cancellationToken);
}
