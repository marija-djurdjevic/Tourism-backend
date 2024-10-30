namespace Explorer.Tours.API.Dtos.TourLifeCycleDtos
{
    public class TourDto
    {
        public enum DifStatus
        {
            Easy,
            Medium,
            Hard
        }

        public enum TStatus
        {
            Draft,
            Published,
            Archived
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TStatus Status { get; set; }

        public DifStatus Difficulty { get; set; }

        public string Tags { get; set; }
        public double Price { get; set; }
        public DateTime PublishedAt {  get; set; }
        public DateTime ArchivedAt { get; set; }
        public double AverageScore {  get; set; }
    }

}

