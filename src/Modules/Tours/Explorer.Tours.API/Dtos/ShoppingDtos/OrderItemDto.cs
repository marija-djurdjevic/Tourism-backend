using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.ShoppingDtos
{
    public class OrderItemDto
    {
        public int TourId { get; set; }
        public double Price { get; set; }
        public string TourName { get; set; }

    }
}
