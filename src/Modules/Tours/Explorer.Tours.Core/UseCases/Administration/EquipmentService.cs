using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    public EquipmentService(ICrudRepository<Equipment> repository, IMapper mapper) : base(repository, mapper) {}

     

    Result<List<EquipmentDto>> IEquipmentService.GetPagedbyTouistrId(Result<List<TouristEquipmentDto>> touristEquipments, int page, int pageSize)
    {

        var allEquipments = MapToDto(CrudRepository.GetPaged(0, 0));

        var touristEquipmentsList = new List<EquipmentDto>();

        foreach (var touristEqupment in touristEquipments.Value)
        {
            touristEquipmentsList.AddRange(allEquipments.Value.Results.ToList().Where(x => x.Id == touristEqupment.EquipmentId).ToList());
        }

        var paginatedResult = touristEquipmentsList
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

        // Vra?anje rezultata sa paginacijom
        return Result.Ok(paginatedResult);
    }

}