using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeAllowances.Queries.Get;
using HumanResourceManagement.Domain.Enums;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;

public class EmployeeCompensationVM
{
    public int Id { get; set; }

    public EntityIdentifier EmployeeProfile { get; set; }
    
    // Calculated Field
    public decimal CurrentGrossSalary { get; set; }
    public decimal BasicSalary { get; set; }
    public List<EmployeeAllowanceVM> EmployeeAllowances { get; set; }    

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod ModeOfPayment { get; set; }
}
