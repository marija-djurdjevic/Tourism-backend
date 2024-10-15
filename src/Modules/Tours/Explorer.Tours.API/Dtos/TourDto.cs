namespace Explorer.Tours.API.Dtos
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
            Canceled,
            Completed
        }
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TourStatus Status { get; set; }

        public DifficultyStatus Difficulty { get; set; }

        public string Tags { get; set; }
        public double Price { get; set; }
    }

}

