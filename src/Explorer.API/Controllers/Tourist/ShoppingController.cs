using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.UseCases.Shopping;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/checkout")]
    public class ShoppingController : BaseApiController
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [HttpPost]
        public ActionResult<ShoppingCart> Checkout([FromBody] List<OrderItemDto> orderItemsDto)
        {
            var touristId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(touristId))
            {
                return Unauthorized();
            }
            var result = _shoppingService.Checkout(orderItemsDto, Int32.Parse(touristId));
            return CreateResponse(result);
        }

    }
}
