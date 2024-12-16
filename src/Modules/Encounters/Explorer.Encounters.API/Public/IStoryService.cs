using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IStoryService
    {
        Result<PagedResult<StoryDto>> GetPaged(int page, int pageSize);
        Result<StoryDto> Get(int id);
        Result<StoryDto> Create(StoryDto story);
        Result Delete(int id);
        Result<StoryDto> Update(StoryDto story);
        Result<StoryDto> GetById(int storyId);

        
    }
}
