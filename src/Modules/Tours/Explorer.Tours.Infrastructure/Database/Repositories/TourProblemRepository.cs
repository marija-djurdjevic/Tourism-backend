using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourProblems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourProblemRepository : CrudDatabaseRepository<TourProblem, ToursContext>, ITourProblemRepository
    {
        public TourProblemRepository(ToursContext dbContext) : base(dbContext) { }

        public Core.Domain.TourProblem Create(Core.Domain.TourProblem entity)
        {
            throw new NotImplementedException();
        }

        public Core.Domain.TourProblem Update(Core.Domain.TourProblem entity)
        {
            throw new NotImplementedException();
        }

        Core.Domain.TourProblem ITourProblemRepository.Get(long id)
        {
            throw new NotImplementedException();
        }

        PagedResult<Core.Domain.TourProblem> ITourProblemRepository.GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}