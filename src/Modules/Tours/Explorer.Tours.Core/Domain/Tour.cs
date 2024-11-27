using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.Core.Domain.Tours;
using System.Xml.Linq;
using static Explorer.Tours.API.Dtos.TourLifecycleDtos.TourDto;

namespace Explorer.Tours.Core.Domain
{
    public enum DifficultyStatus
    {
        Easy,
        Medium,
        Hard
    }

    public enum TourStatus
    {
        Draft,
        Published,
        Archived,
        Closed
    }
    public class Tour : Entity
    {

        public int AuthorId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DifficultyStatus Difficulty { get; private set; }

        public List<string>? Tags { get; private set; }
        public double Price { get; private set; }
        public TourStatus Status { get; private set; }
        public TransportInfo TransportInfo { get; private set; }
        public List<KeyPoint> KeyPoints { get; private set; }
        public DateTime PublishedAt { get; private set; }
        public DateTime ArchivedAt { get; private set; }
        public double AverageScore { get; private set; }
        public List<TourReview> Reviews { get; private set; }
        public Tour(string name, string description, DifficultyStatus difficulty, List<string> tags, double price)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Tags = tags;
            Price = price;
            Status = TourStatus.Draft;
            TransportInfo = new TransportInfo(0, 0, TransportType.Car);
            KeyPoints = new List<KeyPoint>();
            Reviews = new List<TourReview>();
            PublishedAt = DateTime.MinValue;
            ArchivedAt = DateTime.MinValue;
            AverageScore = 0;
        }

        public Tour() { }

        public Tour(string name, string description, DifficultyStatus difficulty, TourStatus status, List<string> tags, double price, int authorId, double averageScore, DateTime publishedAt)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Status = status;
            Tags = tags;
            Price = price;
            AuthorId = authorId;
            AverageScore = averageScore;
            PublishedAt = publishedAt;
            Reviews = new List<TourReview>();
        }
        public Tour(string name, string description, DifficultyStatus difficulty, List<string> tags, double price, int authorId, TransportInfo transportInfo)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Tags = tags;
            Price = price;
            AuthorId = authorId;
            Status = TourStatus.Draft;
            TransportInfo = transportInfo;
            KeyPoints = new List<KeyPoint>();
            PublishedAt = DateTime.MinValue;
            ArchivedAt = DateTime.MinValue;
            AverageScore = 0;
        }
        public void Archive()
        {
            if (Status == TourStatus.Published)
            {
                Status = TourStatus.Archived;
                ArchivedAt = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidOperationException("Only published tours can be archived.");
            }
        }

        public void UpdateTrasnportStatus(double distance, int time)
        {
            TransportInfo.Distance = distance;
            TransportInfo.Time = time;
        }

        public void Publish()
        {
            if (Status != TourStatus.Draft && Status != TourStatus.Archived)
                throw new InvalidOperationException("Only tours in draft or archived status can be published (again).");

            if (!Validate() || !ValidateInput())
                throw new InvalidOperationException("Tour does not meet publishing requirements.");

            Status = TourStatus.Published;
            PublishedAt = DateTime.UtcNow;
        }

        public void CLose()
        {


            Status = TourStatus.Closed;

        }

        public bool Validate()
        {
            return KeyPoints.Count >= 2 && TransportInfo.Time > 0;
        }

        public bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
           !string.IsNullOrWhiteSpace(Description) &&
           Tags.Count != 0 &&
           Price > 0;
        }

        public bool HasKeyPointsInDesiredDistance(Coordinates coordinates, double distance)
        {
            foreach (var kp in KeyPoints)
            {
                if (kp.IsInDesiredDistance(coordinates, distance))
                    return true;
            }
            return false;
        }
    }
}
