using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.ShoppingDtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Discount { get; set; }
        public List<int> TourIds { get; set; }
    }
}
