using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain
{
    public class KeyPoint : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public long TourId { get; private set; }
        public Coordinates Coordinates { get; private set; }
        private KeyPoint() { }
        public KeyPoint(string name, string description, string imagePath, long tourId, Coordinates coordinates)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(imagePath)) throw new ArgumentException("Invalid Image Path.");;
           
            Name = name;
            Description = description;
            ImagePath = imagePath;
            TourId = tourId;
            Coordinates = coordinates;

        }
        public KeyPoint(int id, string name, string description, string imagePath, int tourId, double latitude, double longitude)
        {
            if (id < 0) throw new ArgumentException("Invalid Id.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(imagePath)) throw new ArgumentException("Invalid Image Path.");
            if (longitude < -180 || longitude > 180) throw new ArgumentException("Longitude must be between -180 and 180.");
            if (latitude < -90 || latitude > 90) throw new ArgumentException("Latitude must be between -90 and 90.");
            if (tourId < 0) throw new ArgumentException("Invalid Author Id.");

            Id = id;
            Name = name;
            Description = description;
            ImagePath = imagePath;
            Longitude = longitude;
            Latitude = latitude;
            TourId = tourId;
        }



    }
}
