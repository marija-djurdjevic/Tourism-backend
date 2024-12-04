using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.Shopping
{
    public class Coupon: Entity
    {
        public int AuthorId { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public DateOnly? ExpiryDate { get; set; }//31-12-2199
        public bool AllDiscounted { get; set; }
        public int DiscountedTourId { get; set; }

        public Coupon() { }

        public Coupon(int authorId, string code, int discount, DateOnly? expiryDate, bool allDiscounted, int discountedTourId)
        {
            this.AuthorId = authorId;
            this.Code = code;
            this.Discount = discount;
            this.ExpiryDate = expiryDate ?? new DateOnly(2199, 12, 31);
            this.AllDiscounted = allDiscounted;
            this.DiscountedTourId = discountedTourId;
        }
    }
}
