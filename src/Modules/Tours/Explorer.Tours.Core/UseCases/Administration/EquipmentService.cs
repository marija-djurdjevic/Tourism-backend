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

     Result<EquipmentDto> IEquipmentService.GetById(int id)
    {
        // Dobavljanje paginiranog rezultata, iako ovde koristimo pagedResult bez paginacije (0,0)
        var pagedResult = CrudRepository.GetPaged(0, 0);

        // Mapiranje rezultata u DTO
        var mappedResult = MapToDto(pagedResult);

        // Pronalazak opreme sa datim ID-jem
        var equipment = mappedResult.Value.Results.FirstOrDefault(x => x.Id == id);

        // Ako nije prona?eno, vrati grešku
        if (equipment == null)
        {
            return Result.Fail<EquipmentDto>("Equipment not found");
        }

        // Ako je prona?eno, vrati uspešan rezultat
        return Result.Ok(equipment);
    }

}