using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("all")]
        public ActionResult<List<TourDto>> GetAllTours()
        {
            var result = _tourService.GetAllToursWithKeyPoints();
            return CreateResponse(result);
        }

        [HttpGet("by-author")]
        public ActionResult<PagedResult<TourDto>> GetByAuthorId([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] int id)
        {
            var result = _tourService.GetByAuthorId(page, pageSize, id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult <TourDto> Create([FromBody] TourDto tourDto)
        {
            var result = _tourService.Create(tourDto);
            return CreateResponse(result);
        }

        [HttpPost("publish-tour")]
        public ActionResult<TourDto> Publish([FromBody] TourDto tourDto)
        {
            var result = _tourService.Publish(tourDto);
            return CreateResponse(result);
        }

        [HttpPut("archive-tour")]
        public ActionResult<TourDto> Archive([FromBody] TourDto tourDto)
        {
            var result = _tourService.Archive(tourDto);
            return CreateResponse(result);
        }

        /*[HttpGet("{tourId}/key-points")]
        public ActionResult<List<KeyPointDto>> GetKeyPointsByTourId(int tourId)
        {
            var result = _tourService.GetKeyPointsByTourId(tourId);
            return CreateResponse(result);
        }*/

        [HttpGet("{tourId}/key-points")]
        public TourDto GetKeyPointsByTourId(int tourId)
        {
            var result = _tourService.GetKeyPointsByTourId(tourId);
            return result;
        }

        [HttpPut("{tourId}/transport-info")]
        public ActionResult<bool> UpdateTransportInfo(int tourId, [FromBody] TransportInfoDto transportInfoDto)
        {
            var result = _tourService.UpdateTransportInfo(tourId, transportInfoDto);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("{tourId}/key-points")]
        public async Task<ActionResult<KeyPointDto>> AddKeyPointToTour(int tourId, [FromBody] KeyPointDto keyPointDto)
        {
            var result = _tourService.AddKeyPointToTourAsync(tourId, keyPointDto);

            if (result.IsSuccess)
            {
                return CreateResponse(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TourDto tourDto)
        {
            if (tourDto == null || tourDto.Id != id)
            {
                return BadRequest("Invalid data.");
            }

            var result = _tourService.Update(tourDto);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            if (result.Errors.Any(e => e.Message.Contains("not found")))
            {
                return NotFound("Encounter not found.");
            }

            return BadRequest("Invalid input data.");
        }
    }
}
