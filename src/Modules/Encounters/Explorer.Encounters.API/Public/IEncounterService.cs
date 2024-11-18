using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterService
    {
        Result<PagedResult<EncounterDto>> GetPaged(int page, int pageSize);
        Result<EncounterDto> Create(EncounterDto tour);
        Result Delete(int id);
    }
}
