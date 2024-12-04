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

        public double GetLowestDiscountedPrice(int tourId, double tourPrice)
        {
            var now = DateTime.UtcNow;

            //get active sales
            var activeSales = DbContext.Sales
                .Where(sale => sale.StartTime <= now && sale.EndTime >= now)
                .ToList();

            //get the highest discount for the diven tour
            var highestDiscount = activeSales
                .Where(sale => sale.TourIds.Contains(tourId)) 
                .Select(sale => sale.Discount)               
                .DefaultIfEmpty(0)                           
                .Max();                                      

            var discountedPrice = (tourPrice * (1 - highestDiscount / 100.0));

            return Math.Round(discountedPrice, 2);
        }

        public Sale GetSaleById(int id)
        {
            var sale = DbContext.Sales.FirstOrDefault(c => c.Id == id);

            return sale == null ? throw new Exception("Sale not found.") : sale;
        }
    }
}
