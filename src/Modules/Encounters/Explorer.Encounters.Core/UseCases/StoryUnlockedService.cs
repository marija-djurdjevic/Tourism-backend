using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Secrets;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class StoryUnlockedService : CrudService<StoryUnlockedDto, StoryUnlocked>, IStoryUnlockedService
    {
        private readonly ICrudRepository<StoryUnlocked> _repository;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly IStoryService _storyService;

        public StoryUnlockedService(ICrudRepository<StoryUnlocked> repository, IBookService bookService, IStoryService storyService, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _bookService = bookService;
            _storyService = storyService;
        }

       
         public Result<PagedResult<StoryUnlockedDto>> GetAll(int page, int pageSize)
         {
            var storiesUnlocked = GetPaged(page, pageSize);
            return storiesUnlocked;
         }

       
        public Result<List<StoryUnlockedDto>> GetAllByUserId(int page, int pageSize, int id)
        {
            var storiesUnlocked = GetPaged(page, pageSize);
            var storiesUnlockedForUser = storiesUnlocked.Value.Results.FindAll(x => x.UserId == id);
            return storiesUnlockedForUser;
        }

        //sve korisnikove price
        public Result<List<StoryDto>> GetUserStories(int userId) 
        {
            var stories = GetAllByUserId(0, 0, userId).Value
                   .Select(su => _storyService.GetById(su.StoryId).Value)
                   .ToList();

            return Result.Ok(stories);
        
        }

        //sve korisnikove knjige
        public Result<List<BookDto>> GetUserBooks(int userId)
        {
            var bookResults = GetUserStories(userId).Value
                .Select(s => _bookService.GetById(s.BookId)) 
                .ToList();

            // Filtrirajte samo uspešne rezultate
            var books = bookResults
                .Where(result => result.IsSuccess) 
                .Select(result => result.Value)   
                .ToList();

            return Result.Ok(books); 
        }

    }
}
