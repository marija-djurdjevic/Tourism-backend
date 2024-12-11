using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.EncounterExecutions;
using Explorer.Encounters.Core.Domain.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IBookRepository
    {
        public PagedResult<Book> GetPaged(int page, int pageSize);
        public Book Get(long id);
        public Book Create(Book entity);
        public Book Update(Book entity);
        public void Delete(long id);
    }
}
