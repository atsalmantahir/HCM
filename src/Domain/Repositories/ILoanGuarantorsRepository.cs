namespace HumanResourceManagement.Domain.Repositories;

public interface ILoanGuarantorsRepository
{
    IQueryable<LoanGuarantor> GetAll();
    Task<LoanGuarantor> GetAsync(int id);
    Task<LoanGuarantor> GetAsync(string externalIdentifier);
    Task InsertAsync(LoanGuarantor entity, CancellationToken cancellationToken);
    Task UpdateAsync(LoanGuarantor entity, CancellationToken cancellationToken);
    Task DeleteAsync(LoanGuarantor entity, CancellationToken cancellationToken);
}
