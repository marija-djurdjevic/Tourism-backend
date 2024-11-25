using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Public;
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
    public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService
    {
        private readonly ICrudRepository<Encounter> _repository;
        private IEncounterExecutionService executionService;
        private readonly IMapper _mapper;

        public EncounterService(ICrudRepository<Encounter> repository, IMapper mapper,IEncounterExecutionService encounterExecutionService) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            executionService = encounterExecutionService;
        }

        public Result<List<EncounterDto>> GetPagedForUserAndTour(int userId,int keyPointId)
        {
            var allEncounters = _repository.GetPaged(1, int.MaxValue).Results;
            var encounters = _mapper.Map < List < EncounterDto >> (allEncounters.Where(e => e.KeyPointId == keyPointId && e.Status==Domain.Encounters.EncounterStatus.Active).ToList());
            foreach(var encounter in encounters)
            {
                var execution = executionService.GetByTouristIdAndEncounterId(userId, encounter.Id);
                if (execution != null)
                {
                    encounter.IsCompletedByMe = true;
                }
            }

            return Result.Ok(encounters);
        }
    }
}
