using HumanResourceManagement.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Common.Models;

public class RegisterUser
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }
}
