using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Payments.Core.UseCases.Shopping;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.Authoring;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shopping")]
    public class ShoppingController : BaseApiController
    {
        private readonly IShoppingService _shoppingService;
        private readonly ITourService _tourService;
        private readonly ITourSessionService _sessionService;
        private readonly ITourReviewService _reviewService;

        public ShoppingController(IShoppingService shoppingService, ITourService tourService, ITourSessionService tourSessionService, ITourReviewService tourReviewService)
        {
            _shoppingService = shoppingService;
            _tourService = tourService;
            _sessionService = tourSessionService;
            _reviewService = tourReviewService;
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
                var userCanReview = _sessionService.CanUserReviewTour(id, int.Parse(touristId));

                if (_reviewService.IsTourReviewedByTourist(int.Parse(touristId), id))
                {
                    if (userCanReview)
                    {
                        tour.ReviewStatus = TourDto.TourReviewStatus.Modify;
                    }
                    else
                    {
                        tour.ReviewStatus = TourDto.TourReviewStatus.Reviewed;
                    }
                }
                else if (!userCanReview)
                {
                    tour.ReviewStatus = TourDto.TourReviewStatus.UnableToReview;
                }
                else
                {
                    tour.ReviewStatus = TourDto.TourReviewStatus.WaitForReview;
                }
                tours.Add(tour);
            }

            return CreateResponse(Result.Ok(tours));
        }


    }
}
