using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Stakeholders.Infrastructure.Authentication;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/coupon")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService couponService;

        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet]
        public ActionResult<PagedResult<CouponDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = couponService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{authorId}")]
        public ActionResult<CouponDto> GetByAuthorId(int authorId)
        {
            var result = couponService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CouponDto> Create([FromBody] CouponDto coupon)
        {
            coupon.ExpiryDate ??= new DateOnly(2199, 12, 31);
            coupon.AuthorId = User.PersonId();
            coupon.Code = Guid.NewGuid().ToString("N").Substring(0, 8);
            var result = couponService.Create(coupon);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CouponDto> Update([FromBody] CouponDto coupon)
        {
            var result = couponService.Update(coupon);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CouponDto> Delete(int id)
        {
            var result = couponService.Delete(id);
            return CreateResponse(result);
        }
    }
}
