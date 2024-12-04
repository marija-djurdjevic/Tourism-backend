namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;

public interface IUserRepository
{
    bool Exists(string username);
    User? GetActiveByName(string username);
    User Get(long id);
    User Create(User user);
    long GetPersonId(long userId);
    Person? GetPersonByUserId(long userId);
    Location? GetLocationByUserId(long userId);
    User Update(User user);

    List<User> GetAll();
    List<User> GetAllTourists();

    User? GetUserById(long userId);

}