using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/review")]
    public class TourReviewController : BaseApiController
    {
        

        private readonly ITourReviewService _tourReviewService;
        private readonly ITourService _tourService;
        

        public TourReviewController(ITourReviewService tourReviewService, ITourService tourService)
        {
            _tourReviewService = tourReviewService;
            _tourService = tourService;
        }
        [HttpGet]
        public ActionResult<PagedResult<TourReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourReviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourReviewDto> Create([FromBody] TourReviewDto tourReview)
        {
            tourReview.UserId = User.PersonId();
            tourReview.Username = User.Username();

            var list = _tourService.GetPaged(0, 0);
            if (list.Value.Results.Any(x => x.Id == tourReview.TourId))
            {
                var result = _tourReviewService.Create(tourReview);
                return CreateResponse(result);
            }
           
            return CreateResponse(Result.Fail("Tour with this id doesn't exist"));
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourReviewDto> Update([FromBody] TourReviewDto tourReview)
        {
            var result = _tourReviewService.Update(tourReview);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourReviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}
