using Explorer.BuildingBlocks.Infrastructure.Database;
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
    public class PublishRequestRepository : CrudDatabaseRepository<PublishRequest, ToursContext>, IPublishRequestRepository
    {
        public PublishRequestRepository(ToursContext dbContext) :  base(dbContext) { }
    }
}
