using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Dtos.EncounterExecutionDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.EncounterExecutions;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterExecutionService : BaseService<EncounterExecutionDto, EncounterExecution>, IEncounterExecutionService
    {
        private readonly IMapper _mapper;
        private readonly IEncounterExecutionRepository _encounterExecutionRepository;

        public EncounterExecutionService(IMapper mapper, IEncounterExecutionRepository repository) : base(mapper)
        {
            _mapper = mapper;
            _encounterExecutionRepository = repository;
        }

        public Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterExecutionDto)
        {
            var encounterExecution = _encounterExecutionRepository.Create(MapToDomain(encounterExecutionDto));
            return Result.Ok(MapToDto(encounterExecution));
        }

        public Result<PagedResult<EncounterExecutionDto>> GetPaged(int page, int pageSize)
        {
            var pagedResult = _encounterExecutionRepository.GetPaged(0, 0);
            var encounterExecutions = pagedResult.Results.ToList();

            var mappedResults = MapToDto(encounterExecutions);

            return new PagedResult<EncounterExecutionDto>(
                mappedResults.Value,
                totalCount: mappedResults.Value.Count
            );
        }

        public Result<EncounterExecutionDto> Update(EncounterExecutionDto encounterExecutionDto)
        {
            var encounterExecution = _encounterExecutionRepository.Update(MapToDomain(encounterExecutionDto));
            return Result.Ok(MapToDto(encounterExecution));
        }
        public EncounterExecutionDto? GetByTouristIdAndEncounterId(int touristId, long encounterId)
        {
            return MapToDto(_encounterExecutionRepository.GetPaged(0,0).Results
                .FirstOrDefault(e => e.TouristId == touristId && e.EncounterId == encounterId));
        }
    }
}
