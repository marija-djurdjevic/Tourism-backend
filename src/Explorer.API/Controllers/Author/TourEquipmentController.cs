using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/tourEquipment")]
    public class TourEquipmentController : BaseApiController
    {
        private readonly ITourEquipmentService _tourEquipmentService;
        private readonly IEquipmentService _equipmentService;

        public TourEquipmentController(ITourEquipmentService tourEquipmentService, IEquipmentService equipmentService)
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
        public ActionResult<PagedResult<TourEquipmentDto>> GetEquipmentbyTourId([FromQuery] int tourId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var tourEquipmentList = _tourEquipmentService.GetByTourId(tourId);
            var result = new List<TourEquipmentDto>();
            foreach (var item in tourEquipmentList.Value)
            {
                var equipment = _equipmentService.Get(item.EquipmentId).Value;
                result.Add(new TourEquipmentDto
                {
                    Id = item.Id,
                    EquipmentId = item.Id,
                    TourId = item.TourId,
                    Equipment = equipment
                });
            }
            var paginatedResult = result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return CreateResponse(Result.Ok(paginatedResult));
        }

        [HttpGet("allTourEquipments")]
        public ActionResult<PagedResult<TourEquipmentDto>> GetAllEquipment([FromQuery] int page, [FromQuery] int pageSize)
        {
            var equipments = _equipmentService.GetPaged(0, 0);
            var result = new List<TourEquipmentDto>();
            foreach (var item in equipments.Value.Results)
            {
                result.Add(new TourEquipmentDto
                {
                    EquipmentId = item.Id,
                    Equipment = item
                });
            }
            var paginatedResult = result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return CreateResponse(Result.Ok(result));
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourEquipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
