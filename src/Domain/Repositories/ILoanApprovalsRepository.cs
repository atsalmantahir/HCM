namespace HumanResourceManagement.Domain.Repositories;

public interface ILoanApprovalsRepository
{
    IQueryable<LoanApproval> GetAll();
    Task<LoanApproval> GetAsync(int id);
    Task<LoanApproval> GetAsync(string externalIdentifier);
    Task InsertAsync(LoanApproval entity, CancellationToken cancellationToken);
    Task UpdateAsync(LoanApproval entity, CancellationToken cancellationToken);
    Task DeleteAsync(LoanApproval entity, CancellationToken cancellationToken);
}
