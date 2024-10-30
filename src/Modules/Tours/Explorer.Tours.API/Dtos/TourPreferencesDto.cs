using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Explorer.Tours.API.Dtos.TourLifeCycleDtos.TourDto;

namespace Explorer.Tours.API.Dtos
{
    public class TourPreferencesDto
    {
        public int Id { get; set; }
        public int TouristId {get; set; }
        public DifStatus Difficulty { get; set; }
        public int WalkingRating { get; set; }
        public int CyclingRating { get; set; }
        public int DrivingRating { get; set; }
        public int SailingRating { get; set; }
        public List<string>? Tags { get; set; }
    }
}
