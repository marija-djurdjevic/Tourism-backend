using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : CrudDatabaseRepository<Tour, ToursContext>, ITourRepository
    {
        public TourRepository(ToursContext dbContext) : base(dbContext) { _dbContext = dbContext; }
        private readonly ToursContext _dbContext;
        public Tour GetTourWithKeyPoints(int tourId)
        {
            return _dbContext.Tour
                .Include(t => t.KeyPoints)
                .Include(t => t.Reviews)
                .FirstOrDefault(t => t.Id == tourId);
        }
        public List<Tour> GetAllToursWithKeyPoints()
        {
            return _dbContext.Tour
                .Include(t => t.KeyPoints)
                .Include(t=>t.Reviews)
                .ToList();
        }

        public Tour? GetById(int tourId)
        {
            return _dbContext.Tour
               .FirstOrDefault(t => t.Id == tourId);
        }
        public Tour GetByIdAsync(int tourId)
        {
            return _dbContext.Tour.AsNoTracking().Include(t => t.KeyPoints)
                                        .FirstOrDefault(t => t.Id == tourId);
        }
        public Tour GetKeyPointsForTour(int tourId)
        {
            return _dbContext.Tour.AsNoTracking()
                .Include(t => t.KeyPoints)
                .FirstOrDefault(t => t.Id == tourId);
        }
    }
}