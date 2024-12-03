using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.EncounterExecutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterExecutionRepository
    {
        public PagedResult<EncounterExecution> GetPaged(int page, int pageSize);
        public EncounterExecution Get(long id);
        public EncounterExecution Create(EncounterExecution entity);
        public EncounterExecution Update(EncounterExecution entity);
        public void Update(int id);
        public void Delete(long id);
    }
}
