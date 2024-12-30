using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class StoryRepository : CrudDatabaseRepository<Story, EncountersContext>, IStoryRepository
    {
        private readonly EncountersContext _dbContext;

        public StoryRepository(EncountersContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Story? GetById(int storyId)
        {
            return _dbContext.Stories
               .FirstOrDefault(t => t.Id == storyId);
        }

        public List<Story> GetByBookId(int bookId)
        {
            return _dbContext.Stories
               .Where(s => s.BookId == bookId).ToList();
        }
    }
}
