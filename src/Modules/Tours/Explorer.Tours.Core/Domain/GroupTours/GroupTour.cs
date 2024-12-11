using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.GroupTours
{
    public enum ProgressStatus
    {
        Scheduled,
        InProgress,
        Finished,
        Canceled
    }
    public class GroupTour : Tour
    {
        public int? TouristNumber { get; private set; }

        public DateTime StartTime { get; private set; }
        public int Duration {  get; private set; }
        public ProgressStatus Progress { get; private set; }
        private GroupTour() : base() { }

        public GroupTour(string name, string description, DifficultyStatus difficulty, List<string> tags, double price, int touristNumber, DateTime startTime, int duration) : base(name, description, difficulty, tags, price) {
            TouristNumber = touristNumber;
            StartTime = startTime;
            Duration = duration;
            Progress = ProgressStatus.Scheduled; 
        }
    }
}
