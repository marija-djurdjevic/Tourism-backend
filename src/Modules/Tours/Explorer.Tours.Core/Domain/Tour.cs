using Explorer.BuildingBlocks.Core.Domain;

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
        Canceled,
        Completed
    }
    public class Tour : Entity
    {
        public int Id { get; private set; }
        public int AuthorId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DifficultyStatus Difficulty { get; private set; }  

        public List<string> Tags { get; private set; }
        public double Price { get; private set; }
        public TourStatus Status { get; private set; }

        public Tour(int id, string name, string description, DifficultyStatus difficulty, List<string> tags)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Name.");
            if (tags == null || tags.Count == 0) throw new ArgumentException("Tour must have at least one tag.");
            Id = id;
            Name = name;
            Description = description;
            Difficulty = difficulty;
            Tags = tags;
            Price = 0;
            Status = TourStatus.Draft;
        }
    }
}
