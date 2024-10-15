using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/Administration/tourEquipment")]
    public class TourEquipmentController:BaseApiController
    {
        private readonly ITourEquipmentService _tourEquipmentService;
        private readonly IEquipmentService _equipmentService;

        public TourEquipmentController(ITourEquipmentService tourEquipmentService,IEquipmentService equipmentService)
        {
            _tourEquipmentService = tourEquipmentService;
            _equipmentService = equipmentService;
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] TourEquipmentDto tourEquipment)
        {
            var result = _tourEquipmentService.Create(tourEquipment);
            return CreateResponse(result);
        }

        [HttpGet("byTourId")]
        public ActionResult<PagedResult<EquipmentDto>> GetEquipmentbyTourId([FromQuery] int tourId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var tourEquipmentList = _tourEquipmentService.GetByTourId(tourId);
            var result = _equipmentService.GetPagedbyTourId(tourEquipmentList, page, pageSize);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourEquipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
