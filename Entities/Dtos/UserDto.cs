using System.ComponentModel.DataAnnotations;
namespace Entities.Dtos;

public record UserDto
{
    [Required(ErrorMessage = "UserName is required.")]
    public string UserName { get; init; } 
    [Required(ErrorMessage = "UserName is required.")]
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public HashSet<string> Roles { get; set; } = new HashSet<string>();
}