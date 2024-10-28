using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.TourProblems
{
    public class TourProblem : Entity
    {
        
   
        public int TourId { get; private set; }
        public TourProblem(int tourId)
        {
            TourId = tourId;
        }
    }

}
