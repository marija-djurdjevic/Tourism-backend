using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class WalletRepository: CrudDatabaseRepository<Wallet, PaymentsContext>, IWalletRepository
    {
        private readonly PaymentsContext _dbContext;
        public WalletRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Wallet? GetByTouristId(int touristId)
        {
            return _dbContext.Wallets.FirstOrDefault(w => w.TouristId == touristId);
        }
    }
}
