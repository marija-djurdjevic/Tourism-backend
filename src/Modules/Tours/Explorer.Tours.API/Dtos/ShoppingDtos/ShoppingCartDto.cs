using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.ShoppingDtos
{
    public class ShoppingCartDto
    {
        public int touristId { get; set; }

        public List<OrderItemDto> Items { get; set; }
        public List<TourPurchaseTokenDto> Tokens { get; set; }
        public double TotalPrice { get; set; }

        public ShoppingCartDto()
        {
            Tokens = new List<TourPurchaseTokenDto>();
        }

        public ShoppingCartDto(List<OrderItemDto> items, List<TourPurchaseTokenDto> tokens, double totalPrice)
        {
            Tokens = tokens;
            TotalPrice = totalPrice;
        }
    }
}
