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
using Explorer.Payments.API.Internal.Shopping;
using Explorer.Tours.API.Dtos.TourProblemDtos;

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
        private readonly ITourPurchaseTokenService _purchaseTokenService;
        private readonly INotificationService _notificationService;

        public ShoppingController(IShoppingService shoppingService, ITourService tourService, ITourSessionService tourSessionService, ITourReviewService tourReviewService, ITourPurchaseTokenService purchaseTokenService, INotificationService notificationService)
        {
            _shoppingService = shoppingService;
            _tourService = tourService;
            _sessionService = tourSessionService;
            _reviewService = tourReviewService;
            _purchaseTokenService = purchaseTokenService;
            _notificationService = notificationService;

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

        [HttpGet("refund/{purchaseTokenId:long}")]
        public ActionResult<int> getRefundedTour(int purchaseTokenId)
        {
            var result = _purchaseTokenService.getTourByPurchaseId(purchaseTokenId);
            return CreateResponse(result);
        }

        [HttpPost("refund")]
        public ActionResult<TourDto> Refund([FromBody] int tourId)
        {
            var touristId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(touristId))
            {
                return Unauthorized();
            }

            var refundedTourId = _purchaseTokenService.RefundPurchasedTour(tourId, Int32.Parse(touristId));

            if (refundedTourId.Value == -1)
            {
                return BadRequest(new { Message = "Refund failed. The tour does not exist or has already been refunded." });
            }

            var tour = _tourService.Get(refundedTourId.Value);
            if (tour == null)
            {
                return NotFound(new { Message = "Tour not found." });
            }

            var referenceId = _purchaseTokenService.FindByTourAndTourist(tourId, Int32.Parse(touristId)).Id;
            NotificationDto notificationDto = new NotificationDto("Tour " + tour.Value.Name + " succesfully refunded", NotificationType.TourRefundComment, referenceId, Int32.Parse(touristId), false);
            _notificationService.Create(notificationDto);
            return CreateResponse(tour);
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
