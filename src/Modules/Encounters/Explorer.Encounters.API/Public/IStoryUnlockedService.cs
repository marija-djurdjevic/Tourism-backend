using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IStoryUnlockedService
    {
        Result<PagedResult<StoryUnlockedDto>> GetPaged(int page, int pageSize);
        Result<StoryUnlockedDto> Get(int id);
        Result<StoryUnlockedDto> Create(StoryUnlockedDto storyUnlocked);
        Result Delete(int id);
        Result<StoryUnlockedDto> Update(StoryUnlockedDto storyUnlocked);
    }
}
