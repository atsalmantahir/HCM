using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Enums;

namespace HumanResourceManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Organisation> Organisations { get; }
    DbSet<EmployeeProfile> EmployeeProfiles { get; }
    DbSet<EmployeeCompensation> EmployeeCompensations { get; }
    DbSet<EmployeeEducation> EmployeeEducations { get; }
    DbSet<EmployeeExperience> EmployeeExperiences { get; }
    DbSet<EmployeeAllowance> EmployeeAllowances { get; }
    DbSet<Allowance> Allowances { get; }
    DbSet<Department> Departments { get; }
    DbSet<Designation> Designations { get; }
    DbSet<Holiday> Holidays { get; }
    DbSet<EmployeeAttendance> EmployeeAttendances { get; }
    DbSet<PayrollCycle> PayrollCycles { get; }
    DbSet<Payroll> Payrolls { get; }
    DbSet<TaxSlab> TaxSlabs { get; }
    DbSet<EmployeeLoan> EmployeeLoans { get; }
    DbSet<LoanApproval> LoanApprovals { get; }
    DbSet<LoanGuarantor> LoanGuarantors { get; }
    DbSet<LoanPayment> LoanPayments { get; }
    DbSet<ReviewQuestion> ReviewQuestions { get; }
    DbSet<EmployeeReview> EmployeeReviews { get; }
    DbSet<EmployeeReviewFromManager> EmployeeReviewFromManagers { get; }
    DbSet<EmployeeReviewFromManagerWithQuestionAndAnswer> EmployeeReviewFromManagerWithQuestionAndAnswers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
