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
    public class SaleRepository : CrudDatabaseRepository<Sale, PaymentsContext>, ISaleRepository
    {
        private readonly PaymentsContext _dbContext;
        public SaleRepository(PaymentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Sale GetSaleById(int id)
        {
            var sale = DbContext.Sales.FirstOrDefault(c => c.Id == id);

            return sale == null ? throw new Exception("Sale not found.") : sale;
        }
    }
}
