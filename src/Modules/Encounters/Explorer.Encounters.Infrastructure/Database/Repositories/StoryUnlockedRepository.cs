using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class StoryUnlockedRepository : CrudDatabaseRepository<StoryUnlocked, EncountersContext>, IStoryUnlockedRepository
    {
        private readonly EncountersContext _dbContext;

        public StoryUnlockedRepository(EncountersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
