using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetAllToursWithKeyPoints();
        public Tour GetTourWithKeyPoints(int tourId);

        public Tour GetById(int tourId);
    }
}
