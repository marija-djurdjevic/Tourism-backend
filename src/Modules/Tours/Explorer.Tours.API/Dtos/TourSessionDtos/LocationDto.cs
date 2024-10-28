using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourSessionDtos
{
    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LocationDto(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
