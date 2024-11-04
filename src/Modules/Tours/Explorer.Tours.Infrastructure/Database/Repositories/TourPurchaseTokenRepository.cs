using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourPurchaseTokenRepository : CrudDatabaseRepository<TourPurchaseToken, ToursContext>, ITourPurchaseTokenRepository
    {
        private readonly ToursContext _dbContext;
        public TourPurchaseTokenRepository(ToursContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        public List<int> GetPurchasedTours(int touristId)
        {
            return _dbContext.TourPurchaseTokens
                .Where(token => token.TouristId == touristId)
                .Select(token => token.TourId)
                .ToList();
        }

        public TourPurchaseToken Create(TourPurchaseToken entity)
        {
            _dbContext.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }


    }

}
