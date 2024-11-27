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
    public interface ICouponService
    {
        Result<PagedResult<CouponDto>> GetPaged(int page, int pageSize);
        Result<List<CouponDto>> GetByAuthorId(int auhtorId);
        Result<CouponDto> Create(CouponDto coupon);
        Result<CouponDto> Update(CouponDto coupon);
        Result Delete(int id);
    }
}
