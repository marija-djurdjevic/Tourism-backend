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

        public TourProblem Create(TourProblem entity)
        {
            throw new NotImplementedException();
        }

        public TourProblem Update(TourProblem entity)
        {
            throw new NotImplementedException();
        }

        TourProblem ITourProblemRepository.Get(long id)
        {
            throw new NotImplementedException();
        }

        PagedResult<TourProblem> ITourProblemRepository.GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}