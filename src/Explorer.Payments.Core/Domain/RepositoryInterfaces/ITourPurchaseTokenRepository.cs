using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ITourPurchaseTokenRepository
    {
        public List<int> GetPurchasedTours(int touristId);

        public TourPurchaseToken Create(TourPurchaseToken entity);

    }
}
