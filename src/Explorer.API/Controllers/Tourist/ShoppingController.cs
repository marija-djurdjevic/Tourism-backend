using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Shopping;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shopping")]
    public class ShoppingController : BaseApiController
    {
        private readonly IShoppingService _shoppingService;
        private readonly ITourService _tourService;

        public ShoppingController(IShoppingService shoppingService, ITourService tourService)
        {
            _shoppingService = shoppingService;
            _tourService = tourService;
        }

        [HttpPost("checkout")]
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

        [HttpGet("purchased")]
        public ActionResult<List<TourDto>> GetPurchasedTours()
        {
            List<TourDto> tours = new List<TourDto>();
            var touristId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(touristId))
            {
                return Unauthorized();
            }

            var tourIdsResult = _shoppingService.GetPurchasedToursIds(int.Parse(touristId));

            foreach (int id in tourIdsResult.Value)
            {
                var tour = _tourService.GetById(id).Value;
                tours.Add(tour);
            }

            return CreateResponse(Result.Ok(tours));
        }


    }
}
