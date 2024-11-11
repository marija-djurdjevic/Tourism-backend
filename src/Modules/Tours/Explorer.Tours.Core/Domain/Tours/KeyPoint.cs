using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
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

            Id = id;
            Name = name;
            Description = description;
            ImagePath = imagePath;
            TourId = tourId;
            Coordinates = new Coordinates(latitude, longitude);
        }

        public double GetDistance(Coordinates desiredCoordinates)
        {
            const double EarthRadius = 6371.0; // Poluprečnik Zemlje u kilometrima

            // Konvertovanje koordinata iz stepeni u radijane
            double lat1 = ToRadians(Coordinates.Latitude);
            double lon1 = ToRadians(Coordinates.Longitude);
            double lat2 = ToRadians(desiredCoordinates.Latitude);
            double lon2 = ToRadians(desiredCoordinates.Longitude);

            // Razlika između latituda i longitud
            double latDifference = lat2 - lat1;
            double lonDifference = lon2 - lon1;

            // Primena Haversinove formule
            double a = Math.Pow(Math.Sin(latDifference / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(lonDifference / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Vraćanje distance u kilometrima
            double distance = EarthRadius * c;

            // Vraćamo distancu
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
    }
}
