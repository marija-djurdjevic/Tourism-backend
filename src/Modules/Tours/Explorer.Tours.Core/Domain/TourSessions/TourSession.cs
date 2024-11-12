using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;

namespace Explorer.Tours.Core.Domain.TourSessions
{
    public class TourSession:Entity
    {
        public int TourId { get; private set; }
        public int UserId { get; private set; }
        public Location? CurrentLocation { get; private set; }
        public List<CompletedKeyPoints>?  CompletedKeyPoints { get; private set; }
       


        public DateTime LastActivity { get; private set; }
        public TourSessionStatus Status { get; private set; }

        public DateTime? EndTime { get; private set; }

        private TourSession() { }
        public TourSession(int tourId, Location initialLocation, int userId)
        {
            TourId = tourId;
            CurrentLocation = initialLocation;
            CompletedKeyPoints = new List<CompletedKeyPoints>();
            LastActivity = DateTime.UtcNow;
            Status = TourSessionStatus.Active;
            UserId = userId;
        }


        public void StartSession()
        {
            Status = TourSessionStatus.Active;
           LastActivity = DateTime.UtcNow;
        }


        public void CompleteSession()
        {
            Status = TourSessionStatus.Completed;
            EndTime= DateTime.UtcNow;
            LastActivity = DateTime.UtcNow;
        }

        public void AbandonSession()
        {
            Status = TourSessionStatus.Abandoned;
            EndTime = DateTime.UtcNow;
            LastActivity = DateTime.UtcNow;
        }


        public void UpdateCurrentLocation(Location newLocation)
        {
            CurrentLocation = newLocation;
            LastActivity = DateTime.UtcNow;
        }

        public enum TourSessionStatus
        {
            Active,
            Completed,
            Abandoned
        }


    }
}
