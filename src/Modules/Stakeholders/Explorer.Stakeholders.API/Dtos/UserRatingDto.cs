namespace Explorer.Stakeholders.API.Dtos
{
    public class UserRatingDto
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public string? Username { get; set; }
    }
}
