using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/admin/books")]
    public class BookController : BaseApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("createBook")]
        public ActionResult<BookDto> Create([FromBody] BookDto book)
        {
            book.AdminId = User.PersonId();
            var result = _bookService.Create(book);
            return CreateResponse(result);      
        }

        [HttpGet("forAdmin")]
        public ActionResult<List<BookDto>> GetForAdmin()
        {

            int userId = User.PersonId();
            var results = _bookService.GetForAdmin(userId);
            return CreateResponse(results);
        }
    }
}
