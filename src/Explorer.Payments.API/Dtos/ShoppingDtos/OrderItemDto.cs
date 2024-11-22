using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.ShoppingDtos
{
    public class OrderItemDto
    {

        public int TourId { get; set; }
        public double Price { get; set; }
        public string TourName { get; set; }

        public OrderItemDto() { }
        public OrderItemDto(int tourId, string name,double price)
        {
            TourId = tourId;
            Price = price;
            TourName = name;
        }


    }
}
