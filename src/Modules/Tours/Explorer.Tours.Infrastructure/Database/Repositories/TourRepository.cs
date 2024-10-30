﻿using Explorer.BuildingBlocks.Infrastructure.Database;
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
        public TourRepository(ToursContext dbContext) : base(dbContext) { }// { _dbContext = dbContext;  }
        //private readonly ToursContext _dbContext;
        public Tour GetTourWithKeyPoints(int tourId)
        {
            return DbContext.Tour
                .Include(t => t.KeyPoints)
                .FirstOrDefault(t => t.Id == tourId);
        }
        public List<Tour> GetAllToursWithKeyPoints()
        {
            return DbContext.Tour
                .Include(t => t.KeyPoints!) 
                .ToList();
        }
        public new Tour? Get(int id)
        {
            return DbContext.Tour.Where(t => t.Id == id)
                .Include(t => t.KeyPoints!)
                    //.ThenInclude(s => s.Coordinate)
                .FirstOrDefault();
        }
    }
}