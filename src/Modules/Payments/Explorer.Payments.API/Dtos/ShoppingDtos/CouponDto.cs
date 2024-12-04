using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.ShoppingDtos
{
    public class CouponDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public DateOnly? ExpiryDate { get; set; }//31-12-2199
        public bool AllDiscounted { get; set; }
        public int DiscountedTourId { get; set; }

        public CouponDto() { }
    }
}
