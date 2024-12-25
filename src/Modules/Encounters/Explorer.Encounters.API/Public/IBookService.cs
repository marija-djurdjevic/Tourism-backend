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
    public interface IBookService
    {
        Result<PagedResult<BookDto>> GetPaged(int page, int pageSize);
        Result<BookDto> Get(int id);
        Result<BookDto> Create(BookDto storyUnlocked);
        Result Delete(int id);
        Result<BookDto> Update(BookDto storyUnlocked);

        Result<BookDto> GetById(int bookId);

        Result<List<BookDto>> GetForAdmin(int adminId);
    }
}
