using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.ShoppingDtos
{
    public class BundleDto
    {
        public enum BundleStatus
        {
            Draft,
            Published,
            Archived
        }

        public long Id { get; set; }
        public int AuthorId { get; set; }
        public double Price { get; set; }
        public BundleStatus Status { get; set; }
        public List<int> TourIds { get; set; }
        public string Title { get; set; }
    }
}
