using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourProblems
{
    public class ProblemDetails : ValueObject<ProblemDetails>
    {
        public ProblemCategory Category { get; private set; }
        public int ProblemPriority { get; private set; }
        public string Explanation { get; private set; }
        public DateTime Time { get; private set; }

        [JsonConstructor]
        public ProblemDetails(ProblemCategory category, int problemPriority, string explanation, DateTime time)
        {
            Category = category;
            ProblemPriority = problemPriority;
            Explanation = explanation;
            Time = time;
        }

        protected override bool EqualsCore(ProblemDetails other)
        {
            if (other == null) return false;

            return Category == other.Category &&
                   ProblemPriority == other.ProblemPriority &&
                   Explanation == other.Explanation &&
                   Time == other.Time;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Category, ProblemPriority, Explanation, Time);
        }   
    }

    public enum ProblemCategory
    {
        Other,
        UnclearInstructions,
        RoadObstacles,
        UnreachablePart
    }
}
