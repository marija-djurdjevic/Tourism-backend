namespace Explorer.Tours.API.Dtos.ShoppingDtos
{
    public class TourPurchaseTokenDto
    {
        public int TouristId { get; set; }
        public int TourId { get; set; }

        public TourPurchaseTokenDto() { }

        public TourPurchaseTokenDto(int touristId, int tourId)
        {
            TouristId = touristId;
            TourId = tourId;
        }
    }
}