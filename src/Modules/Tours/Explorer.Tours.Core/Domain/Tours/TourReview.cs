using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TourReview : Entity
    {
        public int Grade { get; private set; }
        public string Comment { get; private set; }

        public int TourId { get; private set; }

        public int UserId { get; private set; }

        public string Username { get; private set; }

        public List<string> Images { get; private set; }

        public DateTime TourVisitDate { get; private set; }

        public DateTime TourReviewDate { get; private set; }
        public int TourProgressPercentage { get; private set; }

        public TourReview(int grade, string comment, int tourId, int userId, string username, List<string> images, DateTime tourVisitDate, DateTime tourReviewDate,int tourProgressPercentage)
        {
            if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Invalid Comment.");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Tourist must have username.");
            if (images.Count()<1) throw new ArgumentException("Tour must have at least one image.");
            if (tourProgressPercentage<0 || tourProgressPercentage>100) throw new ArgumentException("Percentage must be between 0 and 100.");
            Grade = grade;
            Comment = comment;
            TourId = tourId;
            UserId = userId;
            Username = username;
            Images = images;
            TourVisitDate = tourVisitDate;
            TourReviewDate = tourReviewDate;
            TourProgressPercentage=tourProgressPercentage;
        }
    }

}
