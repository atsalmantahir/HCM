using Azure;
using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HumanResourceManagement.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IConfiguration configuration;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        this.configuration = configuration;
        this.userManager = userManager;
        this.roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string email, string userName, string password, string role)
    {
        try
        {
            var userByName = await userManager.FindByNameAsync(userName);
            var userByEmail = await userManager.FindByEmailAsync(email);

            if (userByName != null) 
            {
                return (new Result(succeeded: false, errors: new List<string>
                {
                    $"Username: '{userName}' already exists",
                }), string.Empty);
            }

            if (userByEmail != null)
            {
                return (new Result(succeeded: false, errors: new List<string>
                {
                    $"Email: '{email}' already exists",
                }), string.Empty);
            }

            var user = new ApplicationUser
            {
                UserName = userName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = email,
            };

            if (!ValidateRole(role)) 
            {
                return (new Result(succeeded: false, errors: new List<string> 
                {
                    $"Provided Role: '{role}' does not exists",
                }), string.Empty);
            }

            if (!await roleManager.RoleExistsAsync(Roles.SuperAdmin))
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin));

            if (!await roleManager.RoleExistsAsync(Roles.Administrator))
                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator));

            if (!await roleManager.RoleExistsAsync(Roles.HrManager))
                await roleManager.CreateAsync(new IdentityRole(Roles.HrManager));

            if (!await roleManager.RoleExistsAsync(Roles.Employee))
                await roleManager.CreateAsync(new IdentityRole(Roles.Employee));

            if (!await roleManager.RoleExistsAsync(Roles.User))
                await roleManager.CreateAsync(new IdentityRole(Roles.User));

            var result = await userManager.CreateAsync(user, password).ConfigureAwait(false);

            if (!result.Succeeded)
                return (result.ToApplicationResult(), null);


            if (role == Roles.SuperAdmin && await roleManager.RoleExistsAsync(Roles.SuperAdmin))
            {
                await userManager.AddToRoleAsync(user, Roles.SuperAdmin);
            }

            if (role == Roles.Administrator && await roleManager.RoleExistsAsync(Roles.Administrator))
            {
                await userManager.AddToRoleAsync(user, Roles.Administrator);
            }

            if (role == Roles.HrManager && await roleManager.RoleExistsAsync(Roles.HrManager))
            {
                await userManager.AddToRoleAsync(user, Roles.HrManager);
            }

            if (role == Roles.Employee && await roleManager.RoleExistsAsync(Roles.Employee))
            {
                await userManager.AddToRoleAsync(user, Roles.Employee);
            }

            if (role == Roles.User && await roleManager.RoleExistsAsync(Roles.User))
            {
                await userManager.AddToRoleAsync(user, Roles.User);
            }


            return (result.ToApplicationResult(), user.Id);
        }
        catch 
        {
            return (null, string.Empty);
        }
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user != null && await userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<TokenResult> LoginAsync(LoginUser model) 
    {
        var tokenIssued = new TokenResult();
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    audience: this.configuration["JWT:ValidAudience"],
                    issuer: this.configuration["JWT:ValidIssuer"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

           tokenIssued = new TokenResult
           {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
           };
        }

        return tokenIssued;
    }

    private bool ValidateRole(string role) 
    {
        List<string> existingRoles = new List<string> 
        {
            Roles.User,
            Roles.HrManager,
            Roles.Administrator,
            Roles.SuperAdmin,
            Roles.Employee,
        };

        return existingRoles.Any(r => string.Equals(r, role, StringComparison.OrdinalIgnoreCase));
    }
}
