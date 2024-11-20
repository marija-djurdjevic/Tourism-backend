using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.PublishRequests;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class KeyPointRepository : CrudDatabaseRepository<KeyPoint, ToursContext>, IKeyPointRepository
    {
        private readonly ToursContext _dbContext;
        public KeyPointRepository(ToursContext dbContext) : base(dbContext) { _dbContext = dbContext; }

        public KeyPoint GetByIdAsync(int keyPointId)
        {
            return _dbContext.KeyPoints.AsNoTracking()
                                        .FirstOrDefault(k => k.Id == keyPointId);
        }
    }
}
