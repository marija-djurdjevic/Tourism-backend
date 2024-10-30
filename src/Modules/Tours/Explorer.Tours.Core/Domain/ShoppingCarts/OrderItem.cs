using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class OrderItem : Entity
    {
        public int TourId { get; private set; }
        public double Price { get; private set; }
        public string TourName {  get; private set; }


        public OrderItem(int tourId, double price, string tourName)
        {
            TourId = tourId;
            Price = price;
            TourName = tourName;
        }

    }
}
