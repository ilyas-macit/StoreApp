using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record ResetPasswordDto
{
    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "ConfirmPassword is required.")]
    [Compare("Password",ErrorMessage = "ConfirmPassword and Password must match.")]
    public string ConfirmPassword { get; set; }
}