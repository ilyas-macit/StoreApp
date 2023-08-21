namespace Entities.Dtos;

public record UserDtoForUpdate : UserDto
{
    public HashSet<string> userRoles { get; set; } = new();
}