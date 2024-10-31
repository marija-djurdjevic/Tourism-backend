using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Core.Domain.TourSessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourSessionRepository
    {
        public PagedResult<TourSession> GetPaged(int page, int pageSize);
        public TourSession Get(long id);
        public TourSession Create(TourSession entity);
        public TourSession Update(TourSession entity);
        public void Delete(long id);


    }
}
