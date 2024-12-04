using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/coupon")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService couponService;

        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet("{code}")]
        public ActionResult<CouponDto> GetByCode(string code)
        {
            var result = couponService.GetByCode(code);
            return CreateResponse(result);
        }

        
    }
}
