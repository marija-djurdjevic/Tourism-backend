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

        public List<string> Images { get; set; }

        public DateOnly TourVisitDate { get; set; }

        public DateOnly TourReviewDate { get; set; }
    }
}
