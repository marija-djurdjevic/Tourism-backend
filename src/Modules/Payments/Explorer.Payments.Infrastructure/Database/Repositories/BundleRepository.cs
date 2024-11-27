using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Shopping;
using Explorer.Payments.Core.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class BundleRepository : CrudDatabaseRepository<Bundle, PaymentsContext>, IBundleRepository
    {
        private readonly PaymentsContext _dbContext;
        public BundleRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Bundle>? GetByAuthorId(int authorId)
        {
            return _dbContext.Bundles
                .Where(b => b.AuthorId.Equals(authorId))
                .ToList();
        }
    }
}
