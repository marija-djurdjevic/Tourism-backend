using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IPaymentRecordRepository : ICrudRepository<PaymentRecord>
    {
        List<PaymentRecord>? GetByTouristId(int touristId);
    }
}
