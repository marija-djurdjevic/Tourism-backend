﻿using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourSessions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    internal class TourSessionRepository : CrudDatabaseRepository<TourSession,ToursContext>,ITourSessionRepository
    {
        
        public TourSessionRepository(ToursContext dbContext):base(dbContext) { }
    
    }
}
