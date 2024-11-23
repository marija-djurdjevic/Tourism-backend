using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.EncounterExecutions;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterExecutionRepository : CrudDatabaseRepository<EncounterExecution, EncountersContext>, IEncounterExecutionRepository
    {
        public EncounterExecutionRepository(EncountersContext dbContext) : base(dbContext) { }
    }
}
