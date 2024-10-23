namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRatingRepository
{
    UserRating Create(UserRating user);
    List<UserRating> GetAll();
}

