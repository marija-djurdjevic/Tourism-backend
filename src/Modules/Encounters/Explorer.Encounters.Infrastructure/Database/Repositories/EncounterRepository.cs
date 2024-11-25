using Explorer.BuildingBlocks.Infrastructure.Database;
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
    public class EncounterRepository : CrudDatabaseRepository<Encounter, EncountersContext>, IEncounterRepository
    {
        public EncounterRepository(EncountersContext dbContext) : base(dbContext) { _dbContext = dbContext;  }
        private readonly EncountersContext _dbContext;

        public void Update(long id)
        {
            // Proveri da li postoji već praćena instanca entiteta
            var trackedEntity = _dbContext.Find<Encounter>(id);

            trackedEntity.setStatus(EncounterStatus.Active);
            // Sačuvaj promene u kontekstu
            _dbContext.SaveChanges();
        }

    }
}
