using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Core.Domain.TourSessions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourSessionRepository : CrudDatabaseRepository<TourSession,ToursContext>,ITourSessionRepository
    {
        
        public TourSessionRepository(ToursContext dbContext):base(dbContext) { _dbContext = dbContext; }
        private readonly ToursContext _dbContext;

        public TourSession GetByTourId(long tourId, long userId)
        {
            return _dbContext.TourSessions
                //.Include(ts => ts.CompletedKeyPoints) // Include related data if needed
                .FirstOrDefault(ts => ts.TourId == tourId && ts.UserId == userId);
        }
    }
}
