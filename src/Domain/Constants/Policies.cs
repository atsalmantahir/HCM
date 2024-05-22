namespace HumanResourceManagement.Domain.Constants;

public abstract class Policies
{
    public const string CanPurge = nameof(CanPurge);
    public const string CanCreate = nameof(CanCreate);
    public const string CanView = nameof(CanView);
    public const string CanEdit = nameof(CanEdit);
    public const string CanDelete = nameof(CanDelete);

    public const string AdministratorPolicy = nameof(AdministratorPolicy);
    public const string AdminAndHRManagerPolicy = nameof(AdminAndHRManagerPolicy);
    public const string EmployeePolicy = nameof(EmployeePolicy);
    public const string HRManagerAndEmployeePolicy = nameof(HRManagerAndEmployeePolicy);
}