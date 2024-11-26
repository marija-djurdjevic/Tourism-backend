using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class CouponService : CrudService<CouponDto, Coupon>, ICouponService
    {
        private readonly ICouponRepository couponRepository;
        public CouponService(ICouponRepository couponRepository, IMapper mapper) : base(couponRepository, mapper)
        {
            this.couponRepository = couponRepository;
        }

        public Result<List<CouponDto>> GetByAuthorId(int auhtorId)
        {
            List<Coupon>? coupons = couponRepository.GetByAuthorId(auhtorId);
            if (coupons == null)
            {
                return Result.Fail<List<CouponDto>>("No coupon found for the specified author ID.");
            }
            return MapToDto(coupons);
        }
    }
}
