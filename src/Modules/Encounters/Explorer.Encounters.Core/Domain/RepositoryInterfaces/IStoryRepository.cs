using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IStoryRepository
    {
        public PagedResult<Story> GetPaged(int page, int pageSize);
        public Story Get(long id);
        public Story Create(Story entity);
        public Story Update(Story entity);
        public void Delete(long id);
        public Story GetById(int id);
    }
}
