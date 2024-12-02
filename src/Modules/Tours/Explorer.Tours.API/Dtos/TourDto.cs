using Explorer.Tours.API.Dtos.TourLifeCycleDtos;

namespace Explorer.Tours.API.Dtos.TourLifecycleDtos
{
    public class TourDto
    {
        public enum TourReviewStatus
        {
            WaitForReview,
            Reviewed,
            UnableToReview,
            Modify
        }
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
            Closed,
            Canceled,
            Completed
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TourStatus Status { get; set; }
        public TourReviewStatus? ReviewStatus { get; set; }
        public DifficultyStatus Difficulty { get; set; }

        public List<string>? Tags { get; set; }
        public double Price { get; set; }
        public DateTime PublishedAt {  get; set; }
        public DateTime ArchivedAt { get; set; }
        public double AverageScore {  get; set; }
        public TransportInfoDto? TransportInfo { get; set; }
        public List<KeyPointDto> KeyPoints { get; set; }
        public List<TourReviewDto> Reviews { get; set; }
    }

}

