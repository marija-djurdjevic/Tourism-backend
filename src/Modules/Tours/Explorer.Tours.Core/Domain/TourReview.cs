using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class TourReview : Entity
    {
        public int Grade { get; private set; }
        public string Comment { get; private set; }

        public int TourId { get; private set; }

        public int UserId { get; private set; }

        public string Images { get; private set; }

        public DateOnly TourVisitDate { get; private set; }

        public DateOnly TourReviewDate { get; private set; }

        public TourReview(int grade, string comment, int tourId, int userId, string images, DateOnly tourVisitDate, DateOnly tourReviewDate)
        {
            if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Invalid Comment.");
            if (string.IsNullOrWhiteSpace(images)) throw new ArgumentException("Tour must have at least one image.");
            Grade = grade;
            Comment = comment;
            TourId = tourId;
            UserId = userId;
            Images = images;
            TourVisitDate = tourVisitDate;
            TourReviewDate = tourReviewDate;

        }
    }

}
