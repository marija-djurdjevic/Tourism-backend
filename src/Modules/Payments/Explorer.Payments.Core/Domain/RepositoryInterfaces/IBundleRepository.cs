using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.Core.Domain.Shopping;
using Explorer.Payments.Core.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IBundleRepository : ICrudRepository<Bundle>
    {
        List<Bundle>? GetByAuthorId(int authorId);
    }
}
