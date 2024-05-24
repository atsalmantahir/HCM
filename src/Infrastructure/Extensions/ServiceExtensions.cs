using HumanResourceManagement.Application.Payrolls.Services.Implementations;
using HumanResourceManagement.Application.Payrolls.Services;
using HumanResourceManagement.Domain.Repositories;
using HumanResourceManagement.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HumanResourceManagement.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositeries(this IServiceCollection services)
    {
        services.AddScoped<IAllowancesRepository, AllowancesRepository>();
        services.AddScoped<IOrganisationsRepository, OrganisationsRepository>();
        services.AddScoped<IEmployeeAllowancesRepository, EmployeeAllowancesRepository>();
        services.AddScoped<IEmployeeAttendencesRepository, EmployeeAttendencesRepository>();
        services.AddScoped<IEmployeeCompensationsRepository, EmployeeCompensationsRepository>();
        services.AddScoped<IEmployeeEducationsRepository, EmployeeEducationsRepository>();
        services.AddScoped<IEmployeeExperiencesRepository, EmployeeExperiencesRepository>();
        services.AddScoped<IEmployeeProfilesRepository, EmployeeProfilesRepository>();
        services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
        services.AddScoped<IDesignationsRepository, DesignationsRepository>();
        services.AddScoped<IHolidaysRepository, HolidaysRepository>();
        services.AddScoped<IPayrollCyclesRepository, PayrollCyclesRepository>();
        services.AddScoped<IPayrollsRepository, PayrollsRepository>();
        services.AddScoped<ITaxSlabsRepository, TaxSlabsRepository>();
        services.AddScoped<IEmployeeLoansRepository, EmployeeLoansRepository>();
        services.AddScoped<ILoanApprovalsRepository, LoanApprovalsRepository>();
        services.AddScoped<ILoanGuarantorsRepository, LoanGuarantorsRepository>();
        services.AddScoped<IReviewQuestionsRepository, ReviewQuestionsRepository>();
        services.AddScoped<IEmployeeReviewsRepository, EmployeeReviewsRepository>();
        services.AddScoped<IEmployeeReviewFromManagersRepository, EmployeeReviewFromManagersRepository>();
        services.AddScoped<IEmployeeReviewFromManagerWithQuestionAndAnswersRepository, EmployeeReviewFromManagerWithQuestionAndAnswersRepository>();
    }

    public static void ConfigureServices(this IServiceCollection services) 
    {
        services.AddScoped<IPayrollService, PayrollService>();
        services.AddScoped<IIncomeTaxCalculator, IncomeTaxCalculator>();
    }
}
