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

        public KeyPointStatus Status { get; private set; }
        private KeyPoint() { }
        public KeyPoint(string name, string description, string imagePath, long tourId, Coordinates coordinates)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(imagePath)) throw new ArgumentException("Invalid Image Path."); ;

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

            Id = id;
            Name = name;
            Description = description;
            ImagePath = imagePath;
            TourId = tourId;
            Coordinates = new Coordinates(latitude, longitude);
        }
        //dodala sam ovaj kontruktor zbog sebe :D
        public KeyPoint(string name, string description, string imagePath, long tourId, Coordinates coordinates, KeyPointStatus status)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid Description.");
            if (string.IsNullOrWhiteSpace(imagePath)) throw new ArgumentException("Invalid Image Path."); ;

            Name = name;
            Description = description;
            ImagePath = imagePath;
            TourId = tourId;
            Coordinates = coordinates;
            Status = status;

        }

        public double GetDistance(Coordinates desiredCoordinates)
        {
            const double EarthRadius = 6371.0; 

            double lat1 = ToRadians(Coordinates.Latitude);
            double lon1 = ToRadians(Coordinates.Longitude);
            double lat2 = ToRadians(desiredCoordinates.Latitude);
            double lon2 = ToRadians(desiredCoordinates.Longitude);

           
            double latDifference = lat2 - lat1;
            double lonDifference = lon2 - lon1;

            double a = Math.Pow(Math.Sin(latDifference / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(lonDifference / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

           
            double distance = EarthRadius * c;

            return distance;
        }

        private double ToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }

        public bool IsInDesiredDistance(Coordinates desiredCoordinates, double distance)
        {
            var actualDistance = GetDistance(desiredCoordinates);
            return actualDistance < distance;
        }

        public void UpdateKeyPointStatus(KeyPointStatus status)
        {
           Status = status;
        }
    }
    //dodala sam enum
    public enum KeyPointStatus
    {
        Pending,
        Published  
    }
}