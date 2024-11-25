using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.Shopping
{
    public interface IBundleService
    {
        Result<PagedResult<BundleDto>> GetPaged(int page, int pageSize);
        Result<List<BundleDto>> GetByAuthorId(int auhtorId);
        Result<BundleDto> Create(BundleDto bundle);
        Result<BundleDto> Update(BundleDto bundle);
    }
}
