using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class UserRatingDatabaseRepository : IUserRatingRepository
    {
        private readonly StakeholdersContext _dbContext;

        public UserRatingDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserRating Create(UserRating rating)
        {
            _dbContext.UserRatings.Add(rating);
            _dbContext.SaveChanges();
            return rating;
        }

        public List<UserRating> GetAll()
        {
            return _dbContext.UserRatings.ToList();
        }
    }
}
