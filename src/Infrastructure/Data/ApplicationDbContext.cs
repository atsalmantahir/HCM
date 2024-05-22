using System.Reflection;
using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Domain.Common;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Organisation> Organisations => Set<Organisation>();
    public DbSet<EmployeeProfile> EmployeeProfiles => Set<EmployeeProfile>();
    public DbSet<EmployeeEducation> EmployeeEducations => Set<EmployeeEducation>();
    public DbSet<EmployeeCompensation> EmployeeCompensations => Set<EmployeeCompensation>();
    public DbSet<EmployeeExperience> EmployeeExperiences => Set<EmployeeExperience>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Designation> Designations => Set<Designation>();
    public DbSet<Holiday> Holidays => Set<Holiday>();
    public DbSet<Allowance> Allowances => Set<Allowance>();
    public DbSet<EmployeeAllowance> EmployeeAllowances => Set<EmployeeAllowance>();
    public DbSet<EmployeeAttendance> EmployeeAttendances => Set<EmployeeAttendance>();
    public DbSet<PayrollCycle> PayrollCycles => Set<PayrollCycle>();
    public DbSet<Payroll> Payrolls => Set<Payroll>();
    public DbSet<TaxSlab> TaxSlabs => Set<TaxSlab>();
    public DbSet<EmployeeLoan> EmployeeLoans => Set<EmployeeLoan>();
    public DbSet<LoanApproval> LoanApprovals => Set<LoanApproval>();
    public DbSet<LoanGuarantor> LoanGuarantors => Set<LoanGuarantor>();
    public DbSet<LoanPayment> LoanPayments => Set<LoanPayment>();
    public DbSet<ReviewQuestion> ReviewQuestions => Set<ReviewQuestion>();
    public DbSet<EmployeeReview> EmployeeReviews => Set<EmployeeReview>();
    public DbSet<EmployeeReviewFromManager> EmployeeReviewFromManagers => Set<EmployeeReviewFromManager>();
    public DbSet<EmployeeReviewFromManagerWithQuestionAndAnswer> EmployeeReviewFromManagerWithQuestionAndAnswers => Set<EmployeeReviewFromManagerWithQuestionAndAnswer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
