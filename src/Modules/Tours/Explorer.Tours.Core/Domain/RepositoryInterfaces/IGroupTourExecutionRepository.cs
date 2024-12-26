using Explorer.Tours.Core.Domain.GroupTours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IGroupTourExecutionRepository
    {
        public GroupTourExecution GetById(int touristId, int groupTourId);
    }
}
