using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IStoryUnlockedRepository
    {
        public PagedResult<StoryUnlocked> GetPaged(int page, int pageSize);
        public StoryUnlocked Get(long id);
        public StoryUnlocked Create(StoryUnlocked entity);
        public StoryUnlocked Update(StoryUnlocked entity);
        public void Delete(long id);
    }
}
