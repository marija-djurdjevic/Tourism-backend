using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
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
        public ActionResult<bool> StartTour([FromBody] StartTourDto startTourDto)
        {
            var initialLocation = new LocationDto(startTourDto.Latitude, startTourDto.Longitude);
            var userId = User.PersonId();

            var result = _tourSessionService.StartTour(startTourDto.TourId, userId, initialLocation);

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

        [HttpGet("getKeyPointsByTourId/{tourId}")]
        public ActionResult<List<KeyPointDto>> GetKeyPointsByTourId(int tourId)
        {
            var keyPointsResult = _tourSessionService.GetKeyPointsByTourId(tourId);

            if (keyPointsResult.IsSuccess && keyPointsResult.Value != null && keyPointsResult.Value.Any())
            {
                return Ok(keyPointsResult.Value); // Return the list of key points
            }

            return NotFound($"No key points found for tour ID {tourId}.");
        }


    }

    public class StartTourDto
    {
        public int TourId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int TouristId { get; set; }
    }

}

