using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class TourPurchaseTokenRepository : CrudDatabaseRepository<TourPurchaseToken, PaymentsContext>, ITourPurchaseTokenRepository
    {
        private readonly PaymentsContext _dbContext;
        public TourPurchaseTokenRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<int> GetPurchasedTours(int touristId)
        {
            return _dbContext.TourPurchaseTokens
                .Where(token => token.TouristId == touristId && !token.Refunded)
                .Select(token => token.TourId)
                .ToList();
        }

        public int RefundPurchasedTour(int tourId, int touristId)
        {
            var token = _dbContext.TourPurchaseTokens
                .FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);

            if (!(token == null || token.Refunded))
            {
                token.Refunded = true;
                _dbContext.SaveChanges();
                return token.TourId;
            }
            return -1;
        }

        public TourPurchaseToken FindByTourAndTourist(int tourId, int touristId)
        {
            return _dbContext.TourPurchaseTokens
                .FirstOrDefault(t => t.TourId == tourId && t.TouristId == touristId);
        }

    }

}
