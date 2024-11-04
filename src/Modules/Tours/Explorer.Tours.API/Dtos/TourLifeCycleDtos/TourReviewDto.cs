namespace Explorer.Tours.API.Dtos.TourLifeCycleDtos
{
    public class TourReviewDto
    {
        public int Id { get; set; }

        public int Grade { get; set; }
        public string Comment { get; set; }

        public int TourId { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public List<string> Images { get; set; }

        public DateTime TourVisitDate { get; set; }

        public DateTime TourReviewDate { get; set; }
        public int TourProgressPercentage { get; set; }
    }
}
