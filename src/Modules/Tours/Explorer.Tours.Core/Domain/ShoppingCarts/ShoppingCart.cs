using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
        public List<OrderItem> Items { get; private set; }

        public List<TourPurchaseToken> Tokens { get; protected set; }
        public double TotalPrice { get; private set; }

        public ShoppingCart(List<OrderItem> items,double totalPrice) {
            
            Items = items;
            TotalPrice = totalPrice;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(OrderItem item)
        {
            Items.Remove(item);
        }

        public double GetTotalPrice() {

            double totalPrice = 0;
            foreach(var item in Items)
            {
                totalPrice += item.Price;
            }
            TotalPrice = totalPrice;
            return totalPrice;
        }

        public void Checkout()
        {

        }
    }
}
