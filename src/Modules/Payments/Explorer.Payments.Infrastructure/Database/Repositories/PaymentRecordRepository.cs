using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class PaymentRecordRepository : CrudDatabaseRepository<PaymentRecord, PaymentsContext>, IPaymentRecordRepository
    {
        private readonly PaymentsContext _dbContext;
        public PaymentRecordRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PaymentRecord>? GetByTouristId(int touristId)
        {
            return _dbContext.PaymentRecords
                .Where(pr => pr.TouristId.Equals(touristId))
                .ToList();
        }
    }
}
