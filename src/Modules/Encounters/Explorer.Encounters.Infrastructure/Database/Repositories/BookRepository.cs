using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    internal class BookRepository : CrudDatabaseRepository<Book, EncountersContext>, IBookRepository
    {
        private readonly EncountersContext _dbContext;

        public BookRepository(EncountersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Book? GetById(int bookId)
        {
            return _dbContext.Books
               .FirstOrDefault(t => t.Id == bookId);
        }
    }
}
