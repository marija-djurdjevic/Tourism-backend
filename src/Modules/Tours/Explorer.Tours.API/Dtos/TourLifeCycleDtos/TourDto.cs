namespace Explorer.Tours.API.Dtos.TourLifecycleDtos
{
    public class TourDto
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
            Archived
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TourStatus Status { get; set; }

        public DifficultyStatus Difficulty { get; set; }

        public string Tags { get; set; }
        public double Price { get; set; }
        public DateTime PublishedAt {  get; set; }
        public DateTime ArchivedAt { get; set; }
        public double AverageScore {  get; set; }
        public TransportInfoDto TransportInfo { get; set; }

        public List<KeyPointDto> KeyPoints { get; set; }
    }

}

