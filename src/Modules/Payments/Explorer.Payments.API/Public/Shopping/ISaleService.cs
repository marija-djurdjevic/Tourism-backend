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
    public interface ISaleService
    {
        Result<SaleDto> GetSaleById(int id);
        Result<PagedResult<SaleDto>> GetPaged(int page, int pageSize);
        Result<SaleDto> Create(SaleDto sale);
        Result<SaleDto> Update(SaleDto blog);
        Result Delete(int id);
    }
}
