using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
   // [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/equipment")]
    public class EquipmentController : BaseApiController
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ITouristEquipmentService _touristEquipmentService;

        public EquipmentController(IEquipmentService equipmentService, ITouristEquipmentService touristEquipmentService)
        {
            _equipmentService = equipmentService;
            _touristEquipmentService = touristEquipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] EquipmentDto equipment)
        {
            var result = _equipmentService.Create(equipment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentDto> Update([FromBody] EquipmentDto equipment)
        {
            var result = _equipmentService.Update(equipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _equipmentService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            var result = _equipmentService.GetById(id);
            return CreateResponse(result);
        }

        [HttpGet("byTouristId")]
        public ActionResult<PagedResult<TouristEquipmentDto>> GetEquipmentbyTouristId([FromQuery] int touristId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var touristEquipmentList = _touristEquipmentService.GetByTouristId(touristId);
            var result = new List<EquipmentDto>();
            foreach (var item in touristEquipmentList.Value)
            {
                var equipment = _equipmentService.GetById(item.EquipmentId).Value;
                result.Add(new EquipmentDto
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    Description = equipment.Description
                }) ; 
               // result.Add(equipment);

            }
            var paginatedResult = result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return CreateResponse(Result.Ok(paginatedResult));
        }
    }
}
