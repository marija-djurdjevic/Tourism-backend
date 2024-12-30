using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly StakeholdersContext _dbContext;

        public AchievementRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Achievement? GetById(int id)
        {
            return _dbContext.Achievements.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Achievement> GetAll()
        {
            return _dbContext.Achievements.ToList();
        }

        public Achievement Create(Achievement achievement)
        {
            _dbContext.Achievements.Add(achievement);
            _dbContext.SaveChanges();
            return achievement;
        }

       
        public void Delete(int id)
        {
            var achievement = GetById(id);
           

            _dbContext.Achievements.Remove(achievement);
            _dbContext.SaveChanges();
        }

       
        public void Add(Achievement achievement)
        {
            throw new NotImplementedException();
        }

       

        public void Update(Achievement achievement)
        {
            var existingAchievement = GetById(achievement.Id);

            _dbContext.Entry(existingAchievement).CurrentValues.SetValues(achievement);
            _dbContext.SaveChanges();
          
        }

    }
}
