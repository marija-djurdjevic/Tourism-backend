using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Users;

//[Table("Users", Schema = "stakeholders")]  
public class User : Entity
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; set; }
    public Location Location { get; set; }
    public int XP { get; set; }

    public List<Achievement> Achievements { get; private set; }
    

    public User(string username, string password, UserRole role, bool isActive)
    {
        Username = username;
        Password = password;
        Role = role;
        IsActive = isActive;
        Location = new Location(0, 0);
        Achievements = new List<Achievement>();
        XP = 0;
        Validate();
    }


    public bool SetLocation(float longitude, float latitude)
    {
        if (Role == UserRole.Tourist && IsActive)
        {
            Location = new Location(latitude, longitude);
            return true;
        }
        throw new ArgumentException("Unauthorized attempt to set location.");
    }

    public int getUserLevel()
    {
        return XP / 100+1;
    }

    public void UpdateXPs(int xp)
    {
        if (Role == UserRole.Tourist && IsActive)
        {
            XP += xp;
        }
        else
        {
            throw new ArgumentException("Unauthorized attempt to update XP.");
        }
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Surname");
    }

    public string GetPrimaryRoleName()
    {
        return Role.ToString().ToLower();
    }
}

public enum UserRole
{
    Administrator,
    Author,
    Tourist
}