using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class KeyPointDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ImagePath { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int AuthorId { get; set; }
    }
}
