using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/touristEquipment")]
    public class TouristEquipmentController : BaseApiController
    {
        private readonly ITouristEquipmentService _TouristEquipmentService;

        public TouristEquipmentController(ITouristEquipmentService TouristEquipmentService)
        {
            _TouristEquipmentService = TouristEquipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TouristEquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _TouristEquipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TouristEquipmentDto> Create([FromBody] TouristEquipmentDto TouristEquipment)
        {
            var result = _TouristEquipmentService.Create(TouristEquipment);
            return CreateResponse(result);
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _TouristEquipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
