using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/touristEquipment")]
    public class TouristEquipmentController : BaseApiController
    {
        private readonly ITouristEquipmentService _TouristEquipmentService;
        private readonly IEquipmentService _EquipmentService;

        public TouristEquipmentController(ITouristEquipmentService TouristEquipmentService, IEquipmentService equipmentService)
        {
            _TouristEquipmentService = TouristEquipmentService;
            _EquipmentService = equipmentService;
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

        [HttpGet("byTouristId")]
        public ActionResult<PagedResult<TouristEquipmentDto>> GetEquipmentbyTouristId([FromQuery] int touristId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var touristEquipmentList = _TouristEquipmentService.GetByTouristId(touristId);
            var result = new List<TouristEquipmentDto>();
            foreach (var item in touristEquipmentList.Value)
            {
                var equipment = _EquipmentService.GetById(item.EquipmentId).Value;
                result.Add(new TouristEquipmentDto
                {
                    Id = item.Id,
                    EquipmentId = item.Id,
                    TouristId = item.TouristId,
                    Equipment = equipment
                });

            }
            var paginatedResult = result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return CreateResponse(Result.Ok(paginatedResult));
        }
    }
}
