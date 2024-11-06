using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.TourSessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/administration/tourSession")]
    public class TourSessionController:BaseApiController
    {

        private readonly ITourSessionService _tourSessionService;

        public TourSessionController(ITourSessionService tourSessionService)
        {
            _tourSessionService = tourSessionService;
        }



       


        [HttpPost("start")]
        public ActionResult<bool> StartTour([FromQuery] int tourId, [FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] int touristId)
        {

            var initialLocation = new LocationDto(latitude, longitude);
            var userId = User.PersonId();

            var result = _tourSessionService.StartTour(tourId,userId, initialLocation);


            if (result.IsSuccess)
            {
                return Ok(true);
            }


            return BadRequest(false);
        }



        [HttpPost("complete/{id}")]
        public ActionResult<bool> CompleteTour(int id)
        {
            var userId = User.PersonId();
            var result = _tourSessionService.CompleteTour(id, userId);

            if (result.IsSuccess)
            {
                return Ok(true);
            }

            return NotFound(false);
        }


        [HttpPost("abandon/{id}")]
        public ActionResult<bool> AbandonTour(int id)
        {
            var userId= User.PersonId();
            var result = _tourSessionService.AbandonTour(id,userId);

            if (result.IsSuccess)
            {
                return Ok(true);
            }

            return NotFound(false);
        }



        [HttpPost("update-location")]
        public ActionResult<bool> UpdateLocation([FromQuery] int tourId, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            var location = new LocationDto(latitude,longitude);
            var userId = User.PersonId();
            bool isNear=_tourSessionService.UpdateLocation(tourId, location,userId);

            return Ok(isNear);
        }

        [HttpPost("update-session")]
        public ActionResult UpdateSession([FromQuery] int tourId, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            var locationDto = new LocationDto(latitude, longitude);
            var userId = User.PersonId();
            _tourSessionService.UpdateSession(tourId, locationDto, userId);

            return Ok("Sesija uspešno ažurirana.");
        }

        [HttpPost("updateLastActivity/{id}")]
        public ActionResult<bool> UpdateLastActivity(int id)
        {
            int userId = User.PersonId();
            var result = _tourSessionService.UpdateLastActivity(id, userId);

            if (result.IsSuccess)
            {
                return Ok(true);
            }

            return NotFound(false);
        }

        [HttpGet("getCompletedCheckpoints/{tourId}")]
        public ActionResult<List<CompletedKeyPointDto>> GetCompletedKeyPoints(int tourId)
        {
            int userId = User.PersonId();
            var result = _tourSessionService.GetCompletedKeyPoints(tourId, userId);

            if (result.IsSuccess)
            {
                return Ok(result.Value); // Returns list of completed checkpoints
            }

            return NotFound(result.Errors);
        }

        [HttpPost("addCompleteKeyPoint/{tourId}/{keyPointId}")]
        public ActionResult<bool> CompleteKeyPoint(long tourId, long keyPointId)
        {
            int userId = User.PersonId();
            var result = _tourSessionService.AddCompletedKeyPoint(tourId, keyPointId, userId);

            if (result.IsSuccess)
            {
                return Ok(true);
            }

            return BadRequest(false);
        }

    }


}

