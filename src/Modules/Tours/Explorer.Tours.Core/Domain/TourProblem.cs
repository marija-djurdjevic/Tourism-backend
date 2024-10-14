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
        public DateOnly Date { get; private set; }

        public TourProblem(int tourId, ProblemCategory category, int problemPriority, string description, DateOnly date) 
        {           
            if (problemPriority < 1 || problemPriority > 5)
                throw new ArgumentException("Problem priority must be chosen, a number between 1 and 5.", nameof(problemPriority));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty or whitespace.", nameof(description));

            if (date > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Date cannot be in the future.", nameof(date));

            TourId = tourId;
            Category = category;
            ProblemPriority = problemPriority;
            Description = description;
            Date = date;
        }
    }
   
}
