using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class CouponRepository : CrudDatabaseRepository<Coupon, PaymentsContext>, ICouponRepository
    {
        private readonly PaymentsContext _dbContext;
        public CouponRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Coupon>? GetByAuthorId(int authorId)
        {
            return _dbContext.Coupons
                .Where(b => b.AuthorId.Equals(authorId))
                .ToList();
        }
    }
}