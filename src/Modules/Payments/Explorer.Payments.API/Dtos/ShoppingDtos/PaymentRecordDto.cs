using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.ShoppingDtos
{
    public class PaymentRecordDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int BundleId { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseTime { get; set; }
    }
}
