using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.ShoppingCarts
{
    public class OrderItem : ValueObject<OrderItem>
    {
        public int TourId { get; private set; }
        public double Price { get; private set; }
        public string TourName {  get; private set; }

        [JsonConstructor]
        public OrderItem(int tourId, string tourName, double price)
        {
            TourId = tourId;
            Price = price;
            TourName = tourName;
        }

        protected override bool EqualsCore(OrderItem other)
        {
            return other.TourId == TourId &&
                other.Price == Price &&
                other.TourName == TourName;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(TourId,Price, TourName);
        }
    }
}
