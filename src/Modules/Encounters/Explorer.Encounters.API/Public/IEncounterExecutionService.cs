using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterExecutionDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterExecutionService
    {
        Result<PagedResult<EncounterExecutionDto>> GetPaged(int page, int pageSize);
        Result<EncounterExecutionDto> Create(EncounterExecutionDto encounterExecutionDto);
        Result<EncounterExecutionDto> Update(EncounterExecutionDto encounterExecutionDto);
    }
}
