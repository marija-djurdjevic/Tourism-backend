using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class TourPurchaseToken : ValueObject<TourPurchaseToken>
    {
        public int TouristId { get; set; }
        public int TourId {  get; set; }


        [JsonConstructor]
        public TourPurchaseToken(int touristId,int tourId) {
            TourId = tourId;
            TouristId = touristId;
        }
        protected override bool EqualsCore(TourPurchaseToken other)
        {
            return other.TouristId == TouristId &&
                other.TourId == TourId;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(TouristId, TourId);
        }

    }
}
