using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Shopping
{
    public class PaymentRecord : Entity
    {
        public long TouristId { get; set; }
        public long BundleId { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseTime { get; set; }

        public PaymentRecord() { }

        public PaymentRecord(long touristId, long bundleId, double price, DateTime purhcaseTime)
        {
            TouristId = touristId;
            BundleId = bundleId;
            Price = price;
            PurchaseTime = purhcaseTime;
        }
    }
}
