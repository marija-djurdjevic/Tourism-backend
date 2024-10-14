using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tourPreferences")]
    public class TourPreferencesController : BaseApiController
    {
        private readonly ITourPreferencesService _tourPreferencesService;

        public TourPreferencesController(ITourPreferencesService tourPreferencesService)
        {
            _tourPreferencesService = tourPreferencesService;
        }

        [HttpGet]
        public ActionResult<TourPreferencesDto> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPreferencesService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("GetByTouristId")]
        public ActionResult<TourPreferencesDto> GetByTouristId(int id)
        {
            var result = _tourPreferencesService.GetByTouristId(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourPreferencesDto> Create([FromBody] TourPreferencesDto tourPreferences)
        {
            var result = _tourPreferencesService.Create(tourPreferences);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourPreferencesDto> Update([FromBody] TourPreferencesDto tourPreferences)
        {
            var result = _tourPreferencesService.Update(tourPreferences);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPreferencesService.Delete(id);
            return CreateResponse(result);
        }
    }
}
