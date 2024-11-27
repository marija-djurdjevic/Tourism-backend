using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Shopping
{
    public class Sale : Entity
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int Discount { get; private set; }   //in %
        public List<long> TourIds { get; private set; }
        public Sale()
        {
            
        }
        public Sale(DateTime startTime, DateTime endTime, int discount, List<long> tourIds)
        {
            if (endTime <= startTime)
            {
                throw new ArgumentException("End time must be after start time.");
            }

            if (endTime > startTime.AddDays(14))
            {
                throw new ArgumentException("End time cannot be more than 2 weeks after start time.");
            }

            if (discount < 0 || discount > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discount), "Discount must be between 0 and 100.");
            }

            StartTime = startTime;
            EndTime = endTime;
            Discount = discount;
            TourIds = tourIds ?? new List<long>();
        }
    }
}
