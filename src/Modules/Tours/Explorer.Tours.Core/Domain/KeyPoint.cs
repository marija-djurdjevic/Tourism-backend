using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain
{
    public class KeyPoint : Entity
    {
        public int Id { get; private set; }
        public String Name { get; private set; }
        public String Description { get; private set; }
        public String ImagePath { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set;}
        public int TourId { get; private set; }

        public KeyPoint(int id, string name, string description, string imagePath, double longitude, double latitude, int tourId)
        {
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
