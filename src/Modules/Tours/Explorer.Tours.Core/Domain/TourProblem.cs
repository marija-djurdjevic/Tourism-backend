using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class TourProblem : Entity
    {
        public enum ProblemCategory
        {
            Other,
            UnclearInstructions,
            RoadObstacles,
            UnreachablePart
        }
        public int TourId { get; private set; }
        public  ProblemCategory Category { get; private set; }
        public int ProblemPriority { get; private set; }
        public string Description { get; private set; }
        public TimeOnly Time { get; private set; }

        public TourProblem( int tourId, ProblemCategory category, int problemPriority, string description, TimeOnly time) 
        {           
            if (problemPriority < 1 || problemPriority > 5)
                throw new ArgumentException("Problem priority must be chosen, a number between 1 and 5.", nameof(problemPriority));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty or whitespace.", nameof(description));
            TourId = tourId;
            Category = category;
            ProblemPriority = problemPriority;
            Description = description;
            Time = time;
        }
    }
   
}
