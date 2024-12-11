using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.GroupTours
{
    
    public class GroupTourExecution : Entity
    {
        public int GroupTourId { get; private set; }
        public int TouristId { get; private set; }
        public bool IsFinished { get; private set; }

        public GroupTourExecution() { }

        public GroupTourExecution(int groupTourId, int touristId)
        {
            GroupTourId = groupTourId;
            TouristId = touristId;
            IsFinished = false;
        }

    }
}
