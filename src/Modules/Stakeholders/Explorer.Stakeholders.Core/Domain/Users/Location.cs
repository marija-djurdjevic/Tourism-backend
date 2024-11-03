using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Location: ValueObject<Location>
    {
        public float Latitude { get; }
        public float Longitude { get; }

        [JsonConstructor]
        public Location(float latitude,float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }


        public bool IsNearby(Location other, double radiusInMeters)
        {
            double earthRadius = 6371000; // Earth's radius in meters

            double dLat = (other.Latitude - Latitude) * (Math.PI / 180);
            double dLon = (other.Longitude - Longitude) * (Math.PI / 180);

            double lat1 = Latitude * (Math.PI / 180);
            double lat2 = other.Latitude * (Math.PI / 180);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = earthRadius * c; // Distance in meters

            return distance <= radiusInMeters;
        }

        protected override bool EqualsCore(Location other)
        {
            if (other == null) return false;
            return Longitude == other.Longitude && Latitude == other.Latitude;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Longitude,Latitude);
        }

    }
}
