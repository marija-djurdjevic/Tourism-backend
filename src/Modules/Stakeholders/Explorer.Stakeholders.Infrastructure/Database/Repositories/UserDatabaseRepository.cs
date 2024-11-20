﻿using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;

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
}