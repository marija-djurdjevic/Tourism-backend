using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourProblemDto
    {
        public enum ProblemCategory
        {
            Other,
            UnclearInstructions,
            RoadObstacles,
            UnreachablePart
        }
        public int TourId { get;  set; }
        public ProblemCategory Category { get;  set; }
        public int ProblemPriority { get;  set; }
        public string Description { get;  set; }
        public DateOnly Date { get;  set; }

    }
    
}
