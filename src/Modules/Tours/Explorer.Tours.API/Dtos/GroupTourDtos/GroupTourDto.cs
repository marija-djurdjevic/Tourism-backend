using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.GroupTourDtos
{
    public enum ProgressStatus
    {
        Scheduled,
        InProgress,
        Finished,
        Canceled
    }
    public class GroupTourDto : TourDto
    {
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public int? TouristNumber { get; set; }
        public ProgressStatus Progress { get; set; }

    }
}
