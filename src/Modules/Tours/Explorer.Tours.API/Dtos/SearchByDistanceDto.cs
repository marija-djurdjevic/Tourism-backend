using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class SearchByDistanceDto
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Distance { get; set; }
    }
}
