using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    bool Exists(string username);
    User Get(long id);
    User? GetActiveByName(string username);
    User Create(User user);
    long GetPersonId(long userId);
    Person? GetPersonByUserId(long userId);
    Location? GetLocationByUserId(long userId);
    User Update(User user);
}