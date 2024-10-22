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
        private readonly ITouristEquipmentService _touristEquipmentService;
        private readonly IEquipmentService _equipmentService;

        public TouristEquipmentController(ITouristEquipmentService TouristEquipmentService, IEquipmentService equipmentService)
        {
            _touristEquipmentService = TouristEquipmentService;
            _equipmentService = equipmentService;
        }

       /* [HttpGet]
        public ActionResult<PagedResult<TouristEquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _TouristEquipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }*/

        [HttpPost]
        public ActionResult<TouristEquipmentDto> Create([FromBody] TouristEquipmentDto TouristEquipment)
        {
            var result = _touristEquipmentService.Create(TouristEquipment);
            return CreateResponse(result);
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _touristEquipmentService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("byTouristId")]
        public ActionResult<PagedResult<TouristEquipmentDto>> GetEquipmentbyTouristId([FromQuery] int touristId)
        {
            var touristEquipmentList = _touristEquipmentService.GetByTouristId(touristId);
            var result = new List<TouristEquipmentDto>();
            foreach (var item in touristEquipmentList.Value)
            {
                var equipment = _equipmentService.Get(item.EquipmentId).Value;
                result.Add(new TouristEquipmentDto
                {
                    Id = item.Id,
                    EquipmentId = item.Id,
                    TouristId = item.TouristId,
                    Equipment = equipment
                });

            }
            
            return CreateResponse(Result.Ok(result));
        }

       

        [HttpGet]
        public ActionResult<PagedResult<EquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }



    }
}
