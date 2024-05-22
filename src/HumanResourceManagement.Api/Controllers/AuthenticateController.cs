using HumanResourceManagement.Application.Common.Interfaces;
using HumanResourceManagement.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanResourceManagement.Api.Controllers;

/// <summary>
/// Authenticate controller
/// </summary>

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IIdentityService identityService;

    public AuthenticateController(IIdentityService identityService)
    {
        this.identityService = identityService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUser registerUser) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await this
            .identityService
            .CreateUserAsync(registerUser.Email, registerUser.Username, registerUser.Password, registerUser.Role);

        if (result.Result?.Succeeded == false)
        {
            return BadRequest(result.Result);
        }

        return Ok($"user with id: {result.UserId} has been registerd");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IResult> LoginUserAsync([FromBody] LoginUser loginUser)
    {
        Log.Information("Login User");
        var tokenReult = await this.identityService.LoginAsync(loginUser);

        if (tokenReult.Token == null) 
        {
            return Results.BadRequest("Incorrect email/password");
        }

        return Results.Ok(tokenReult);
    }

}
