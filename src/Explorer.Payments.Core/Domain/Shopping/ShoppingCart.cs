using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
       
        public int TouristId { get; set; }
        public List<OrderItem> Items { get; private set; }
        public double TotalPrice { get; private set; }

        public ShoppingCart() { }
        public ShoppingCart(int touristId, List<OrderItem> items,List<TourPurchaseToken> tokens)
        {
            TouristId = touristId;
            Items = items;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(OrderItem item)
        {
            Items.Remove(item);
        }

        public void CalculatePrice() {

            double totalPrice = 0;
            foreach(var item in Items)
            {
                totalPrice += item.Price;
            }
            TotalPrice = totalPrice;
        }
    }
}
