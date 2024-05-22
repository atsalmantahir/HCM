using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Constants;

namespace HumanResourceManagement.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string email, string userName, string password, string role);

    Task<Result> DeleteUserAsync(string userId);

    Task<TokenResult> LoginAsync(LoginUser model);
}
