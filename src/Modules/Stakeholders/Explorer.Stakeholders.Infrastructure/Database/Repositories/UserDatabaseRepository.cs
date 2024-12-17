using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class UserDatabaseRepository : CrudDatabaseRepository<User, StakeholdersContext>, IUserRepository
{
    private readonly StakeholdersContext _dbContext;

    public UserDatabaseRepository(StakeholdersContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(string username)
    {
        return _dbContext.Users.Any(user => user.Username == username);
    }

    public User? GetActiveByName(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
    }
    public User? Get(long id)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Id == id && user.IsActive);
    }

    public User Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public User Update(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
        return user;
    }

    public User UpdateAchievements(ICollection<Achievement> achievements, long userId)
    {
        if (achievements.Count > 0)
        {
            var existingUser = _dbContext.Users
                .Include(u => u.Achievements)
                .FirstOrDefault(u => u.Id == userId);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            // Učitajte postojeće achievement-e iz baze
            _dbContext.Entry(existingUser).Collection(u => u.Achievements).Load();

            // Očistite trenutne achievement-e korisnika
            existingUser.Achievements.Clear();

            // Pronađite ili priložite postojeće achievement-e
            foreach (var achievement in achievements)
            {
                var existingAchievement = _dbContext.Achievements.Find(achievement.Id);
                if (existingAchievement == null)
                {
                    throw new Exception($"Achievement with ID {achievement.Id} not found");
                }

                existingUser.Achievements.Add(existingAchievement);
            }

            // Sačuvajte promene
            _dbContext.SaveChanges();

            return existingUser;
        }

        return null;
    }


    public long GetPersonId(long userId)
    {
        var person = _dbContext.People.FirstOrDefault(i => i.UserId == userId);
        if (person == null) throw new KeyNotFoundException("Not found.");
        return person.Id;
    }

    public Person? GetPersonByUserId(long userId)
    {
        return _dbContext.People.FirstOrDefault(i => i.UserId == userId);
    }

    public Location? GetLocationByUserId(long userId)
    {
        return _dbContext.Users.FirstOrDefault(i => i.Id == userId && i.IsActive && i.Role == UserRole.Tourist)?.Location;
    }


    public List<User> GetAll()
    {
        return _dbContext.Users.ToList();
    }

    public List<User> GetAllTourists()
    {
        return _dbContext.Users.Where(u => u.Role == UserRole.Tourist).ToList();
    }
    public User? GetUserById(long userId)
    {
        return _dbContext.Users.Include(a => a.Achievements).FirstOrDefault(i => i.Id == userId);
    }
}