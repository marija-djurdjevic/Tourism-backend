using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TouristEquipmentService : CrudService<TouristEquipmentDto, TouristEquipment>, ITouristEquipmentService
{
    public TouristEquipmentService(ICrudRepository<TouristEquipment> repository, IMapper mapper) : base(repository, mapper) { }

   
    

    Result<List<TouristEquipmentDto>> ITouristEquipmentService.GetByTouristId(int touristId)
    {
        var pagedResult = CrudRepository.GetPaged(0, 0);

        var mappedResult = MapToDto(pagedResult);

        var list = mappedResult.Value.Results.ToList().Where(x => x.TouristId == touristId).ToList();

        return Result.Ok(list);
    }
}
