using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourPreferences : Entity
    {
        public int Id { get; private set; }
        public int TouristId { get; private set; }
        public DifficultyStatus? Difficulty { get; private set; }
        public int WalkingRating { get; private set; }
        public int CyclingRating { get; private set; }
        public int DrivingRating { get; private set; }
        public int SailingRating { get; private set; }
        public List<string>? Tags { get; private set; }

        public TourPreferences(int id, int touristId, DifficultyStatus? difficulty, int walkingRating, int cyclingRating, int drivingRating, int sailingRating, List<string>? tags)
        {
            if (walkingRating < 0 || walkingRating > 3) throw new ArgumentException("Invalid Walking Rating.");
            if (cyclingRating < 0 || cyclingRating > 3) throw new ArgumentException("Invalid Cycling Rating.");
            if (drivingRating < 0 || drivingRating > 3) throw new ArgumentException("Invalid Driving Rating.");
            if (sailingRating < 0 || sailingRating > 3) throw new ArgumentException("Invalid Sailing Rating.");
            Id = id;
            TouristId = touristId;
            Difficulty = difficulty;
            WalkingRating = walkingRating;
            CyclingRating = cyclingRating;
            DrivingRating = drivingRating;
            SailingRating = sailingRating;
            Tags = tags;
        }
    }
}
