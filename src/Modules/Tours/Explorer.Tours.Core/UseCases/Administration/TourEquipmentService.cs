using Explorer.BuildingBlocks.Core.UseCases;
using System;
using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourEquipmentService : CrudService<TourEquipmentDto, TourEquipment>, ITourEquipmentService
    {
        public TourEquipmentService(ICrudRepository<TourEquipment> repository, IMapper mapper) : base(repository, mapper) { }

        Result<List<TourEquipmentDto>> ITourEquipmentService.GetByTourId(int tourId)
        {
            var pagedResult = CrudRepository.GetPaged(0, 0);

            var mappedResult = MapToDto(pagedResult);

            var list = mappedResult.Value.Results.ToList().Where(x => x.TourId == tourId).ToList();

            return Result.Ok(list);
        }
    }
}
