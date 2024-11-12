using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.TourProblems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourProblemRepository
    {
        public PagedResult<TourProblem> GetPaged(int page, int pageSize);
        public TourProblem Get(long id);
        public TourProblem Create(TourProblem entity);
        public TourProblem Update(TourProblem entity);
        public void Delete(long id);

    }
}
