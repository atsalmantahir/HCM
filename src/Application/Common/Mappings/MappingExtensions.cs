using HumanResourceManagement.Application.Allowances.Commands.Create;
using HumanResourceManagement.Application.Allowances.Queries.Get;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Departments.Commands.Create;
using HumanResourceManagement.Application.Departments.Commands.Update;
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.Designations.Commands.Create;
using HumanResourceManagement.Application.Designations.Commands.Update;
using HumanResourceManagement.Application.Designations.Queries.Get;
using HumanResourceManagement.Application.EmployeeAllowances.Commands.Create;
using HumanResourceManagement.Application.EmployeeAllowances.Queries.Get;
using HumanResourceManagement.Application.EmployeeAttendences.Commands.Create;
using HumanResourceManagement.Application.EmployeeAttendences.Commands.Update;
using HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;
using HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;
using HumanResourceManagement.Application.EmployeeCompensations.Commands.Services;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Create;
using HumanResourceManagement.Application.EmployeeEducations.Commands.Update;
using HumanResourceManagement.Application.EmployeeEducations.Queries.Get;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Create;
using HumanResourceManagement.Application.EmployeeExperiences.Commands.Update;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;
using HumanResourceManagement.Application.EmployeeLoans.Commands.Create;
using HumanResourceManagement.Application.EmployeeLoans.Queries.Get;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Create;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Update;
using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Application.Holidays.Commands.Create;
using HumanResourceManagement.Application.Holidays.Commands.Update;
using HumanResourceManagement.Application.Holidays.Queries.Get;
using HumanResourceManagement.Application.LoanApprovals.Queries.Get;
using HumanResourceManagement.Application.LoanGuarantors.Queries.Get;
using HumanResourceManagement.Application.Organisations.Commands.Create;
using HumanResourceManagement.Application.Organisations.Commands.Update;
using HumanResourceManagement.Application.Organisations.Queries.Get;
using HumanResourceManagement.Application.Payrolls.Commands.Create;
using HumanResourceManagement.Application.Payrolls.Queries.Get;
using HumanResourceManagement.Application.ReviewQuestions.Commands.Create;
using HumanResourceManagement.Application.ReviewQuestions.Queries.Get;
using HumanResourceManagement.Application.TaxSlabs.Commands.Create;
using HumanResourceManagement.Application.TaxSlabs.Commands.Update;
using HumanResourceManagement.Application.TaxSlabs.Queries.Get;
using HumanResourceManagement.Domain.Entities;

namespace HumanResourceManagement.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();

    public static Allowance ToEntity(this CreateAllowanceCommand request) 
    {
        return request == null ? null : new Allowance 
        {
            Name = request.Name,
        };
    }

    public static AllowanceVM ToDto(this Allowance request)
    {
        return request == null ? null : new AllowanceVM
        {
            Name = request.Name,
            Description = "Field not set yet",
            Id = request.AllowanceId,
            IsTaxable = false,

        };
    }

    public static IQueryable<AllowanceVM> ToDto(this IQueryable<Allowance> requests) 
    {
        return requests == null ? null : requests.Select(x => x.ToDto());
    }

    public static DepartmentVM ToDto(this Department department) 
    {
        return department != null ? new DepartmentVM
        {
            Id = department.DepartmentId,
            DepartmentName = department.DepartmentName,
            Organisation = new OrganisationVM 
            {
                Id = department.Organisation.OrganisationId,
                OrganisationName = department.Organisation.OrganisationName,
                Address = department.Organisation.Address,
                DailyWorkingHours = department.Organisation.DailyWorkingHours,
                Logo = department.Organisation.Logo,
                TimeIn = department.Organisation.TimeIn,
                TimeOut = department.Organisation.TimeOut,
            },
        } : null;
    }

    public static IQueryable<DepartmentVM> ToDto(this IQueryable<Department> departments)
    {
        if (departments == null)
        {
            return null;
        }

        return departments.Select(x => new DepartmentVM
        {
            Id = x.DepartmentId,
            DepartmentName = x.DepartmentName,
        });
    }

    public static CreatePayrollCommand ToDto(this Domain.Entities.Payroll payroll) 
    {
        if (payroll == null)
        {
            return null;
        }

        return new CreatePayrollCommand
        {
            BasicSalary = payroll.GrossSalary,
            Deductions = payroll.Deductions,
            EmployeeProfileId = payroll.EmployeeProfileId,
            HasHealthInsurance = payroll.HasHealthInsurance,
            HasRetirementPlan = payroll.HasRetirementPlan,
            HolidayHours = payroll.HolidayHours,
            HolidayPay = payroll.HolidayPay,
            HolidayRate = payroll.HolidayRate,
            HoursWorked = payroll.HoursWorked,
            NetSalary = payroll.NetSalary,
            OvertimeHours = payroll.OvertimeHours,
            OvertimePay = payroll.OvertimePay,
            OvertimeRate = payroll.OvertimeRate,
            PayrollDate = payroll.PayrollDate,
            TotalEarnings = payroll.TotalEarnings
        };
    }

    public static Domain.Entities.Payroll ToEntity(this CreatePayrollCommand createPayrollCommand)
    {
        if (createPayrollCommand == null)
        {
            return null;
        }

        return new Domain.Entities.Payroll
        {
            TotalEarnings = createPayrollCommand.TotalEarnings,
            PayrollDate = createPayrollCommand.PayrollDate,
            OvertimeRate = createPayrollCommand.OvertimeRate,
            OvertimePay = createPayrollCommand.OvertimePay,
            OvertimeHours = createPayrollCommand.OvertimeHours,
            NetSalary = createPayrollCommand.NetSalary,
            HoursWorked = createPayrollCommand.HoursWorked,
            HolidayRate = createPayrollCommand.HolidayRate,
            HolidayPay = createPayrollCommand.HolidayPay,
            GrossSalary = createPayrollCommand.BasicSalary,
            Deductions = createPayrollCommand.Deductions,
            EmployeeProfileId = createPayrollCommand.EmployeeProfileId,
            HasHealthInsurance = createPayrollCommand.HasHealthInsurance,
            HasRetirementPlan = createPayrollCommand.HasRetirementPlan,
            HolidayHours = createPayrollCommand.HolidayHours,
        };
    }

    public static CreateOrganisationCommand ToDto(this Organisation organisation) 
    {
        if (organisation == null) 
        {
            return null;
        }

        return new CreateOrganisationCommand 
        {
            Address = organisation.Address,
            DailyWorkingHours = organisation.DailyWorkingHours,
            IsActive = organisation.IsActive,
            Logo = organisation.Logo,
            OrganisationName = organisation.OrganisationName,
            TimeIn = organisation.TimeIn,
            TimeOut = organisation.TimeOut,
            WeekendHolidays = organisation.WeekendHolidays
        };
    }

    public static Organisation ToCreateOrganisationEntity(this CreateOrganisationCommand organisation)
    {
        if (organisation == null)
        {
            return null;
        }

        return new Organisation
        {
            Address = organisation.Address,
            DailyWorkingHours = organisation.DailyWorkingHours,
            IsActive = organisation.IsActive,
            Logo = organisation.Logo,
            OrganisationName = organisation.OrganisationName,
            TimeIn = organisation.TimeIn,
            TimeOut = organisation.TimeOut,
            WeekendHolidays = organisation.WeekendHolidays,
        };
    }

    public static Organisation ToUpdateOrganisationEntity(this UpdateOrganisationCommand organisation, int organisationId)
    {
        if (organisation == null)
        {
            return null;
        }

        return new Organisation
        {
            OrganisationId = organisationId,
            Address = organisation.Address,
            DailyWorkingHours = organisation.DailyWorkingHours,
            IsActive = organisation.IsActive,
            Logo = organisation.Logo,
            OrganisationName = organisation.OrganisationName,
            TimeIn = organisation.TimeIn,
            TimeOut = organisation.TimeOut,
            WeekendHolidays = organisation.WeekendHolidays,
        };
    }

    public static EmployeeCompensation ToEntity(this CreateEmployeeCompensationCommand request, int employeeProfileId) 
    {
        if (request == null) 
        {
            return null;
        }

        return new EmployeeCompensation
        {
            EmployeeProfileId = employeeProfileId,
            ModeOfPayment = request.ModeOfPayment,
            BasicSalary = request.BasicSalary
        };
    }

    public static EmployeeCompensation ToEntity(this CreateEmployeeCompensationCommand request, int employeeProfileId, List<EmployeeAllowance> employeeAllowances)
    {
        if (request == null)
        {
            return null;
        }

        return new EmployeeCompensation
        {
            EmployeeProfileId = employeeProfileId,
            ModeOfPayment = request.ModeOfPayment,
            BasicSalary = request.BasicSalary,
            EmployeeAllowances = employeeAllowances
        };
    }


    public static EmployeeCompensation ToEntity(this UpdateEmployeeCompensationCommand request, int employeeProfileId, int employeeCompensationId)
    {
        if (request == null)
        {
            return null;
        }

        return new EmployeeCompensation
        {
            EmployeeProfileId = employeeProfileId,
            EmployeeCompensationId = employeeCompensationId,
            BasicSalary = request.BasicSalary,
            ModeOfPayment = request.ModeOfPayment,
        };
    }

    public static Department ToEntity(this CreateDepartmentCommand request, int organisationId)
    {
        if (request == null)
        {
            return null;
        }

        return new Department
        {
            OrganisationId = organisationId,
            DepartmentName = request.DepartmentName,
        };
    }

    public static Department ToEntity(this UpdateDepartmentCommand request, int organisationId, int departmentId)
    {
        if (request == null)
        {
            return null;
        }

        return new Department
        {
            DepartmentId = departmentId,
            OrganisationId = organisationId,
            DepartmentName = request.DepartmentName,
        };
    }

    public static Designation ToEntity(this CreateDesignationCommand request, int departmentId)
    {
        if (request == null)
        {
            return null;
        }

        return new Designation
        {
            DepartmentId = departmentId,
            DesignationName = request.DesignationName,
        };
    }

    public static Designation ToEntity(this UpdateDesignationCommand request, int departmentId, int designationId)
    {
        if (request == null)
        {
            return null;
        }

        return new Designation
        {
            DepartmentId = departmentId,
            DesignationId = designationId,
            DesignationName = request.DesignationName,
        };
    }

    public static DesignationVM ToDto(this Designation designation)
    {
        return designation != null ? new DesignationVM
        {
            DesignationName = designation.DesignationName,
            Department = new DepartmentVM 
            {
                Id = designation.Department.DepartmentId,
                DepartmentName = designation.Department.DepartmentName,
            },
        } : null;
    }

    public static IQueryable<DesignationVM> ToDesignationListDto(this IQueryable<Designation> designations)
    {
        return designations?.Select(x => x.ToDto());
    }

    public static OrganisationVM ToQueryOrganisationDto(this Organisation organisation)
    {
        return organisation != null ? new OrganisationVM
        {
            Id = organisation.OrganisationId,
            Address = organisation.Address,
            DailyWorkingHours = organisation.DailyWorkingHours,
            IsActive = organisation.IsActive,
            Logo = organisation.Logo,
            OrganisationName = organisation.OrganisationName,
            TimeIn = organisation.TimeIn,
            TimeOut = organisation.TimeOut,
            WeekendHolidays = organisation.WeekendHolidays,
        } : null;
    }

    public static IQueryable<OrganisationVM> ToQueryOrganisationListDto(this IQueryable<Organisation> organisations)
    {
        return organisations?.Select(x => x.ToQueryOrganisationDto());
    }

    public static EmployeeEducation ToEmployeeEducationEntity(this CreateEmployeeEducationCommand request, int employeeProfileId) 
    {
        return request == null ? null : new EmployeeEducation 
        {
            EmployeeProfileId = employeeProfileId,
            CompletionYear = request.CompletionYear,
            Degree = request.Degree,
            Institution = request.Institution,
        }; 
    }

    public static EmployeeExperience ToEmployeeExperienceEntity(this CreateEmployeeExperienceCommand request, int employeeProfileId)
    {
        return request == null ? null : new EmployeeExperience
        {
            EmployeeProfileId = employeeProfileId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CompanyName = request.CompanyName,
            Position = request.Position,
        };
    }

    public static EmployeeExperience ToEmployeeExperienceEntity(
        this UpdateEmployeeExperienceCommand request, 
        int employeeProfileId,
        int employeeExperienceId)
    {
        return request == null ? null : new EmployeeExperience
        {
            EmployeeExperienceId = employeeExperienceId,
            EmployeeProfileId = employeeProfileId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CompanyName = request.CompanyName,
            Position = request.Position,
        };
    }

    public static EmployeeProfile ToEmployeeProfileEntity(
        this UpdateEmployeeProfileCommand request,
        int employeeProfileId,
        int designationId,
        string emailAddress) 
    {
        return request == null ? null : new EmployeeProfile
        {
            EmployeeProfileId = employeeProfileId,
            DesignationId = designationId,
            ActiveStatus = request.ActiveStatus,
            Contact = request.Contact,
            EmailAddress = emailAddress,
            EmployeeCode = request.EmployeeCode,
            EmployeeName = request.EmployeeName,
            EmployeeType = request.EmployeeType,
            Gender = request.Gender,
            LineManager = request.LineManager,
            Segment = request.Segment,
            LastWorkingDayDate = request.LastWorkingDayDate,
            MaritalStatus = request.MaritalStatus,
            JoiningDate = request.JoiningDate,
        };
    }

    public static EmployeeProfile ToEmployeeProfileEntity(
        this CreateEmployeeProfileCommand request,
        int designationId)
    {
        return request == null ? null : new EmployeeProfile
        {
            DesignationId = designationId,
            ActiveStatus = request.ActiveStatus,
            Contact = request.Contact,
            EmailAddress = request.EmailAddress,
            EmployeeCode = request.EmployeeCode,
            EmployeeName = request.EmployeeName,
            EmployeeType = request.EmployeeType,
            Gender = request.Gender,
            LineManager = request.LineManager,
            Segment = request.Segment,
            LastWorkingDayDate = request.LastWorkingDayDate,
            MaritalStatus = request.MaritalStatus,
            JoiningDate = request.JoiningDate,
        };
    }

    public static TaxSlab ToEntity(this CreateTaxSlabCommand request) 
    {
        return request == null ? null : new TaxSlab 
        {
            ValidFrom = request.ValidFrom,
            ValidTill = request.ValidTill,
            MinimumIncome = request.MinimumIncome,
            MaximumIncome = request.MaximumIncome,
            BaseTax = request.BaseTax,
            PercentageTax = request.PercentageTax,
            ExcessAmount = request.ExcessAmount,
        };
    }

    public static TaxSlab ToEntity(this UpdateTaxSlabCommand request, int taxSlabID)
    {
        return request == null ? null : new TaxSlab
        {
            TaxSlabId = taxSlabID,
            ValidFrom = request.ValidFrom,
            ValidTill = request.ValidTill,
            MinimumIncome = request.MinimumIncome,
            MaximumIncome = request.MaximumIncome,
            BaseTax = request.BaseTax,
            PercentageTax = request.PercentageTax,
            ExcessAmount = request.ExcessAmount,
        };
    }

    public static TaxSlabVM ToDto(this TaxSlab request)
    {
        return request == null ? null : new TaxSlabVM
        {
            ValidFrom = request.ValidFrom,
            ValidTill = request.ValidTill,
            MinimumIncome = request.MinimumIncome,
            MaximumIncome = request.MaximumIncome,
            BaseTax = request.BaseTax,
            PercentageTax = request.PercentageTax,
            ExcessAmount = request.ExcessAmount,
        };
    }

    public static IQueryable<TaxSlabVM> ToDto(this IQueryable<TaxSlab> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static Holiday ToEntity(this CreateHolidayCommand request) 
    {
        return request == null ? null : new Holiday 
        {
            HolidayDate = request.HolidayDate,
            HolidayName = request.HolidayName,
            IsActive = request.IsActive,
            IsOfficial = request.IsOfficial            
        };
    }

    public static Holiday ToEntity(this UpdateHolidayCommand request, int holidayId)
    {
        return request == null ? null : new Holiday
        {
            HolidayId = holidayId,
            HolidayDate = request.HolidayDate,
            HolidayName = request.HolidayName,
            IsActive = request.IsActive,
            IsOfficial = request.IsOfficial
        };
    }

    public static HolidayVM ToDto(this Holiday request)
    {
        return request == null ? null : new HolidayVM
        {
            Id = request.HolidayId,
            HolidayDate = request.HolidayDate,
            HolidayName = request.HolidayName,
            IsActive = request.IsActive,
            IsOfficial = request.IsOfficial
        };
    }

    public static IQueryable<HolidayVM> ToDto(this IQueryable<Holiday> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static EmployeeAttendance ToEntity(this CreateEmployeeAttendenceCommand request, int employeeProfileId)
    {
        if (request == null)
            return null;

        DateOnly attendanceDate;
        if (!DateOnly.TryParse(request.AttendanceDate, out attendanceDate))
        {
            // Handle parsing failure, such as returning null or throwing an exception
            throw new ArgumentException("Invalid AttendanceDate format");
        }

        TimeOnly timeIn;
        if (!TimeOnly.TryParse(request.TimeIn, out timeIn))
        {
            // Handle parsing failure, such as returning null or throwing an exception
            throw new ArgumentException("Invalid TimeIn format");
        }

        TimeOnly timeOut;
        if (!TimeOnly.TryParse(request.TimeOut, out timeOut))
        {
            // Handle parsing failure, such as returning null or throwing an exception
            throw new ArgumentException("Invalid TimeOut format");
        }

        return request == null ? null : new EmployeeAttendance
        {
            EmployeeProfileId = employeeProfileId,
            AttendanceDate = attendanceDate,
            TimeIn = timeIn,
            TimeOut = timeOut,
            ApprovedBy = 0,
            IsApproved = true,
        };
    }

    public static EmployeeAttendance ToEntity(this UpdateEmployeeAttendenceCommand request, int employeeProfileId, int employeeAttendenceId) 
    {
        return request == null ? null : new EmployeeAttendance 
        {
            EmployeeAttendanceId = employeeAttendenceId,
            EmployeeProfileId = employeeProfileId,
            AttendanceDate = request.AttendanceDate,
            TimeIn = request.TimeIn,
            TimeOut = request.TimeOut,
        };
    }

    public static EmployeeAttendenceVM ToDto(this EmployeeAttendance request)
    {
        return request == null ? null : new EmployeeAttendenceVM
        {
            EmployeeProfile = new EntityIdentifier 
            {
                Id = request.EmployeeProfile.EmployeeProfileId,
            },
            AttendanceDate = request.AttendanceDate,
            TimeIn = request.TimeIn,
            TimeOut = request.TimeOut,
        };
    }

    public static IQueryable<EmployeeAttendenceVM> ToDto(this IQueryable<EmployeeAttendance> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static EmployeeEducation ToEntity(this UpdateEmployeeEducationCommand request, int employeeProfileId, int employeeEducationId) 
    {
        return request == null ? null : new EmployeeEducation 
        {
            CompletionYear = request.CompletionYear,
            EmployeeEducationId = employeeEducationId,
            EmployeeProfileId = employeeProfileId,
            Institution = request.Institution,
            Degree = request.Degree,
        };
    }

    public static EmployeeEducationVM ToDto(this EmployeeEducation request)
    {
        return request == null ? null : new EmployeeEducationVM
        {
            CompletionYear = request.CompletionYear,
            Institution = request.Institution,
            Degree = request.Degree,
            EmployeeProfile = new EntityIdentifier 
            {
                Id = request.EmployeeProfile.EmployeeProfileId,
            }
        };
    }

    public static IList<EmployeeEducationVM> ToDto(this IList<EmployeeEducation> request)
    {
        return request == null ? null : request.Select(x => x.ToDto()).ToList();
    }

    public static EmployeeCompensationVM ToDto(this EmployeeCompensation request)
    {
        decimal grossSalary = CalculateGrossSalary(request);

        return request == null ? null : new EmployeeCompensationVM
        {
            BasicSalary = request.BasicSalary,
            EmployeeProfile = new EntityIdentifier
            {
                Id = request.EmployeeProfile.EmployeeProfileId,
            },
            CurrentGrossSalary = grossSalary,
            Id = request.EmployeeCompensationId,
            ModeOfPayment = request.ModeOfPayment,
            EmployeeAllowances = request.EmployeeAllowances == null
            ? null : request.EmployeeAllowances
            .Select(x => new EmployeeAllowanceVM
            {
                Allowance = new AllowanceVM
                {
                    Id = x.Allowance.AllowanceId,
                    Name = x.Allowance?.Name,
                    Description = "Field not set yet",
                    IsTaxable = false,
                },
                Amount = x.Amount,
            })
            .ToList(),
        };
    }

    public static decimal CalculateGrossSalary(EmployeeCompensation request)
    {
        decimal totalAllowancesAmount = request.EmployeeAllowances?.Sum(x => x.Amount) ?? 0;
        decimal grossSalary = request.BasicSalary + totalAllowancesAmount;
        return grossSalary;
    }

    public static IQueryable<EmployeeCompensationVM> ToDto(this IQueryable<EmployeeCompensation> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static EmployeeExperienceVM ToDto(this EmployeeExperience request) 
    {
        return request == null ? null : new EmployeeExperienceVM
        {
            CompanyName = request.CompanyName,
            EmployeeProfile = new EntityIdentifier 
            {
                Id = request.EmployeeProfile.EmployeeProfileId,
            },
            Id = request.EmployeeExperienceId,
            EndDate = request.EndDate,
            Position = request.Position,
            StartDate = request.StartDate,
        };
    }

    public static IQueryable<EmployeeExperienceVM> ToDto(this IQueryable<EmployeeExperience> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static EmployeeProfileVM ToDto(this EmployeeProfile request) 
    {
        return request == null ? null : new EmployeeProfileVM 
        {
            ActiveStatus = request.ActiveStatus,
            Contact = request.Contact,
            LastWorkingDayDate = request.LastWorkingDayDate,
            JoiningDate = request.JoiningDate,
            EmailAddress = request.EmailAddress,
            EmployeeCode = request.EmployeeCode,
            EmployeeName = request.EmployeeName,
            EmployeeType = request.EmployeeType,
            Id = request.EmployeeProfileId,
            Gender = request.Gender,
            MaritalStatus = request.MaritalStatus,
            LineManager = request.LineManager,
            Segment = request.Segment,
            Designation = request.Designation == null ? null : new DesignationVM 
            {
                Id = request.Designation.DesignationId,
                DesignationName = request.Designation.DesignationName,
            },
            EmployeeCompensation = request.EmployeeCompensation == null ? null : request.EmployeeCompensation.ToDto(),
            EmployeeEducations = request.EmployeeEducations == null ? new () : request.EmployeeEducations.Select(x => new EmployeeEducationVM 
            {
                Id = x.EmployeeEducationId,
                CompletionYear = x.CompletionYear,
                Degree = x.Degree,
                EmployeeProfile = new EntityIdentifier 
                {
                    Id = request.EmployeeProfileId,
                },
                Institution = x.Institution
            }).ToList(),
            EmployeeExperiences = request.EmployeeExperiences == null ? new () : request.EmployeeExperiences.Select(x => new EmployeeExperienceVM
            {
                Id = x.EmployeeExperienceId,
                EmployeeProfile = new EntityIdentifier 
                {
                    Id = request.EmployeeProfileId,
                },
                CompanyName = x.CompanyName,
                EndDate = x.EndDate,
                Position = x.Position,
                StartDate = x.StartDate,
            }).ToList(),
        };
    }

    public static IQueryable<EmployeeProfileVM> ToDto(this IQueryable<EmployeeProfile> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static PayrollVM ToPayrollVMDto(this Domain.Entities.Payroll request) 
    {
        return request == null ? null : new PayrollVM 
        {
            GrossSalary = request.GrossSalary,
            Deductions = request.Deductions,
            EmployeeProfile = request.EmployeeProfile.ToDto(),
            Id = request.PayrollId,
            HasHealthInsurance = request.HasHealthInsurance,
            HasRetirementPlan = request.HasRetirementPlan,
            HolidayHours = request.HolidayHours,
            HolidayPay = request.HolidayPay,
            HolidayRate = request.HolidayRate,
            HoursWorked = request.HoursWorked,
            NetSalary = request.NetSalary,
            OvertimeHours = request.OvertimeHours,
            RequiredHours = request.RequiredHours,
            OvertimePay = request.OvertimePay,
            OvertimeRate = request.OvertimeRate,
            PayrollDate = request.PayrollDate,
            TotalEarnings = request.TotalEarnings,
            AmountPaid = request.AmountPaid,
            PaymentDate = request.PaymentDate,
            PaymentMethod = request.PaymentMethod,
            PaymentStatus = request.PaymentStatus,
            RejectionReason = request.RejectionReason,
        };
    }

    public static IQueryable<PayrollVM> ToPayrollsVMDto(this IQueryable<Domain.Entities.Payroll> request)
    {
        return request == null ? null : request.Select(x => x.ToPayrollVMDto());
    }

    public static EmployeeLoan ToEmployeeLoanEntity(this CreateEmployeeLoanCommand request, int employeeProfileId) 
    {
        return request == null ? null : new EmployeeLoan
        {
            EmployeeProfileId = employeeProfileId,
            DisbursementDate = request.DisbursementDate,
            LoanAmount = request.LoanAmount,
            LoanType = request.LoanType,
            PaybackEndDate = request.PaybackEndDate,
            PaybackInterval = request.PaybackInterval,
            PaybackStartDate = request.PaybackStartDate,
            PaymentStatus = request.PaymentStatus,
            RepaymentStatus = request.RepaymentStatus,
            LoanGuarantors = request.LoanGuarantors?.Select(x => new LoanGuarantor
            {
                Name = x.Name,
                Relationship = x.Relationship,
                ContactInfo = x?.ContactInfo,
            }).ToList(),
            LoanApprovals = request.LoanApprovals?.Select(x => new LoanApproval 
            {
                ApprovalDate = x.ApprovalDate,
                ApproverDesignation = x.ApproverDesignation,
                ApproverName = x.ApproverName,
                Comments = x.Comments,
                LoanApprovalStatus = x.LoanApprovalStatus,
                Reason = x.Reason,
                Title = x.Title,
                
            }).ToList(),
        };
    }

    public static EmployeeLoanVM ToDto(this EmployeeLoan request) 
    {
        return request == null ? null : new EmployeeLoanVM 
        {
            LoanType = request.LoanType,
            DisbursementDate = request.DisbursementDate,
            EmployeeProfile = request.EmployeeProfile?.ToDto(),
            Id = request.EmployeeLoanID,
            LoanAmount = request.LoanAmount,
            LoanGuarantors = request.LoanGuarantors?.Select(x => new LoanGuarantorVM
            {
                Name = x.Name,
                Relationship = x.Relationship,
                ContactInfo = x?.ContactInfo,
            }).ToList(),
            LoanApprovals = request.LoanApprovals?.Select(x => new LoanApprovalVM 
            {
                ApprovalDate = x.ApprovalDate,
                ApproverDesignation = x.ApproverDesignation,
                ApproverName= x.ApproverName,
                Comments = x.Comments,
                Id = x.LoanApprovalID,
                LoanApprovalStatus = x.LoanApprovalStatus,
                Reason = x.Reason,
                Title = x.Title,
            }).ToList(),
            PaybackEndDate = request.PaybackEndDate,
            PaybackStartDate = request.PaybackStartDate,
            RepaymentStatus = request.RepaymentStatus,
            PaymentStatus = request.PaymentStatus,
            PaybackInterval = request.PaybackInterval,
        };
    }


    public static IQueryable<EmployeeLoanVM> ToDto(this IQueryable<EmployeeLoan> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static LoanApprovalVM ToDto(this LoanApproval request) 
    {
        return request == null ? null : new LoanApprovalVM 
        {
            ApprovalDate = request.ApprovalDate,
            ApproverDesignation = request.ApproverDesignation,
            ApproverName = request.ApproverName,
            Comments = request.Comments,
            Id = request.LoanApprovalID,
            LoanApprovalStatus = request.LoanApprovalStatus,
            Reason = request.Reason,
            Title = request.Title,
        };
    }

    public static IQueryable<LoanApprovalVM> ToDto(this IQueryable<LoanApproval> request) 
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }

    public static LoanGuarantorVM ToDto(this LoanGuarantor request) 
    {
        return request == null ? null : new LoanGuarantorVM 
        {
            ContactInfo = request.ContactInfo,
            Id = request.LoanGuarantorID,
            Name = request.Name,
            Relationship = request.Relationship,
        };
    }

    public static ReviewQuestion ToEntity(this CreateReviewQuestionCommand reqeust) 
    {
        return reqeust == null ? null : new ReviewQuestion 
        {
            IsActive = true,
            MaxValue = reqeust.MaxValue,
            MinValue = reqeust.MinValue,
            Text = reqeust.Text,
        };
    }

    public static ReviewQuestionVM ToDto(this ReviewQuestion request) 
    {
        return request == null ? null : new ReviewQuestionVM 
        {
            Id = request.ReviewQuestionId,
            IsActive = request.IsActive,
            MaxValue = request.MaxValue,
            MinValue = request.MinValue,
            Text = request.Text,
        };
    }

    public static IQueryable<ReviewQuestionVM> ToDto(this IQueryable<ReviewQuestion> request)
    {
        return request == null ? null : request.Select(x => x.ToDto());
    }
}
