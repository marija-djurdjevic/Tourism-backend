using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class KeyPoint : Entity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public int TourId { get; private set; }
        public Coordinates Coordinates { get; private set; }

        private KeyPoint() { }
        public KeyPoint(int id, string name, string description, string imagePath, int tourId, Coordinates coordinates)
        {
            if (id < 0) throw new ArgumentException("Invalid Id.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(imagePath)) throw new ArgumentException("Invalid Image Path.");
            if (tourId < 0) throw new ArgumentException("Invalid Author Id.");
           

            Id = id;
            Name = name;
            Description = description;
            ImagePath = imagePath;
            TourId = tourId;
            Coordinates = coordinates;

        }

       

    }
}
