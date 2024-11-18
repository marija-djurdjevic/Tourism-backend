using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class ShoppingCartRepository : CrudDatabaseRepository<ShoppingCart, PaymentsContext>, IShoppingCartRepository
    {

        public ShoppingCartRepository(PaymentsContext dbContext) : base(dbContext) { }


        
    
    }
}
