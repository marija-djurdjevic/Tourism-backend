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
        public int Id { get; set; }
        public int TourId {  get; set; }
        public DateTime PurchaseDate { get; set; }


        [JsonConstructor]
        public TourPurchaseToken(int id,int tourId,DateTime purchaseDate) {
            Id = id;
            TourId = tourId;
            PurchaseDate = purchaseDate;
        }
        protected override bool EqualsCore(TourPurchaseToken other)
        {
            return other.Id == Id && 
                other.TourId == TourId && 
                other.PurchaseDate == PurchaseDate;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Id,TourId,PurchaseDate);
        }

    }
}
