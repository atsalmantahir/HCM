﻿using HumanResourceManagement.Application.Payrolls.Services.Models;

namespace HumanResourceManagement.Application.Payrolls.Services;

public interface IPayrollService
{
    Task GeneratePayrollAsync(PayrollRequest request);
}
