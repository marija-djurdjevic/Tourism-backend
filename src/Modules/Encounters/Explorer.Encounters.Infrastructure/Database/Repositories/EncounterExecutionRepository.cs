using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.EncounterExecutions;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterExecutionRepository : CrudDatabaseRepository<EncounterExecution, EncountersContext>, IEncounterExecutionRepository
    {
        private readonly EncountersContext _dbContext;

        public EncounterExecutionRepository(EncountersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(int id)
        {
            var trackedEntity = _dbContext.Find<EncounterExecution>((long)id);
            trackedEntity.setCompletedTime(DateTime.UtcNow);
            _dbContext.SaveChanges();
        }
    }
}
