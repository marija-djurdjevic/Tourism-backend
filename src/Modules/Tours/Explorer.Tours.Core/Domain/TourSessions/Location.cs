using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;


namespace Explorer.Tours.Core.Domain.TourSessions
{
    public class Location:ValueObject<Location>
    {

        public double Longitude { get; private set; }
        public double Latitude { get; private set; }




        [JsonConstructor]
        public Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        protected override bool EqualsCore(Location other)
        {
            return Latitude == other.Latitude && Longitude == other.Longitude;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Latitude, Longitude);
        }


        public static bool IsWithinSimpleDistance(Location location1, Location location2, double threshold = 2)
        {
            
            return Math.Abs(location1.Latitude - location2.Latitude) <= threshold &&
                   Math.Abs(location1.Longitude - location2.Longitude) <= threshold;
        }
    }
}

