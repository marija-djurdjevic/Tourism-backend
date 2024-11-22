using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class SearchByDistanceDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }

        public SearchByDistanceDto(double latitude, double longitude, double distance)
        {
            Latitude = latitude;
            Longitude = longitude;
            Distance = distance;
        }
    }
}
