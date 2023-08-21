using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record UserDtoForCreation : UserDto
{
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; }
}