using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public int Id { get; set; }

        public int Grade { get; set; }
        public string Comment { get; set; }

        public int TourId { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Images { get; set; }

        public DateTime TourVisitDate { get; set; }

        public DateTime TourReviewDate { get; set; }
    }
}
