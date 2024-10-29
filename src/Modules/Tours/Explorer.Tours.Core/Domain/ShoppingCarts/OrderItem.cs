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

        public int Quantity { get; private set; }

        public OrderItem(int id,double price, string name) {
            TourId = id;
            Price = price;
            TourName = name;
        }
    }
}
