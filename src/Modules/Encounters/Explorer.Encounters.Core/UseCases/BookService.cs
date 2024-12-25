using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.Secrets;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class BookService : CrudService<BookDto, Book>, IBookService
    {
        private readonly ICrudRepository<Book> _repository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(ICrudRepository<Book> repository,  IMapper mapper, IBookRepository bookRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _bookRepository = bookRepository;

        }

        //public Result<PagedResult<BookDto>> GetByAuthorId(int page, int pageSize, int id)
        //{
        //    var tours = GetPaged(page, pageSize);
        //    var filteredResults = tours.Value.Results
        //        .Where(x => x.AuthorId == id)
        //        .ToList();

        //    var pagedAuthorTours = new PagedResult<TourDto>(filteredResults, filteredResults.Count);

        //    return tours.WithValue(pagedAuthorTours);
        //}

        public Result<BookDto> GetById(int bookId)
        {
            // return  _mapper.Map( _storyRepository.GetById(storyId);
            return MapToDto(_bookRepository.GetById(bookId));
        }

        public Result<List<BookDto>> GetForAdmin(int adminId)
        {
            var books = GetPaged(0, 0);
            var booksForAdmin = books.Value.Results.FindAll(x => x.AdminId == adminId);
            return booksForAdmin;
        }

    }
}
