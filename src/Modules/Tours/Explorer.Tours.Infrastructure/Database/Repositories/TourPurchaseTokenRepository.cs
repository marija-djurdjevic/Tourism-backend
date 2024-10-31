using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourPurchaseTokenRepository : CrudDatabaseRepository<TourPurchaseToken, ToursContext>, ITourPurchaseTokenRepository
    {

        public TourPurchaseTokenRepository(ToursContext dbContext) : base(dbContext) { }

    }
    
}
