using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourSessionDtos
{
    public class TourSessionDto
    {
        public int TourId { get; set; }
        public LocationDto CurrentLocation { get; set; }
        public List<CompletedKeyPointDto> CompletedKeyPoints { get; set; }
        public DateTime LastActivity { get; set; }
        public TourSessionStatus Status { get; private set; }

        public DateTime? EndTime { get; private set; }


        public TourSessionDto(int tourId, LocationDto currentLocation, List<CompletedKeyPointDto> completedKeyPoints, DateTime lastActivity, TourSessionStatus tourSessionStatus)
        {
            TourId = tourId;
            CurrentLocation = currentLocation;
            CompletedKeyPoints = completedKeyPoints;
            LastActivity = lastActivity;
            Status = tourSessionStatus;
            
        }


        public enum TourSessionStatus
        {
            Active,
            Completed,
            Abandoned
        }
    }
}
