
namespace Explorer.Stakeholders.API.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
    public LocationDto? Location { get; set; }
    public int? XP { get; set; }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}
