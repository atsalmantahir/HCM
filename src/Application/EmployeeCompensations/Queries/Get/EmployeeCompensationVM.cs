using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Enums;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;

public class EmployeeCompensationVM
{
    public string ExternalIdentifier { get; set; }
    public EntityExternalIdentifier EmployeeProfile { get; set; }
    public decimal CurrentGrossSalary { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal HouseRentAllowance { get; set; }
    public decimal MedicalAllowance { get; set; }
    public decimal UtilityAllowance { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod ModeOfPayment { get; set; }
}
