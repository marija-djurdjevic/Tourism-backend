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



        //[HttpPost("start")]
        //public ActionResult<TourSessionDto> StartTour(int tourId, double latitude, double longitude)
        //{
            
        //    var initialLocation = new LocationDto(latitude, longitude);

           
        //    var result = _tourSessionService.StartTour(tourId, initialLocation);

            
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Value);
        //    }

           
        //    return BadRequest(result.Errors.Select(e => e.Message));
        //}



        //[HttpPost("complete/{id}")]
        //public ActionResult<TourSessionDto> CompleteTour(int id)
        //{
        //    var result = _tourSessionService.CompleteTour(id);

        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Value);
        //    }

        //    return NotFound(result.Errors.Select(e => e.Message));
        //}

        
        //[HttpPost("abandon/{id}")]
        //public ActionResult<TourSessionDto> AbandonTour(int id)
        //{
        //    var result = _tourSessionService.AbandonTour(id);

        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Value);
        //    }

        //    return NotFound(result.Errors.Select(e => e.Message));
        //}

       
       
    }


}

