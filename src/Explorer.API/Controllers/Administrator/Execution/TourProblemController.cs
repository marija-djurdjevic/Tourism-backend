using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.Core.UseCases.Execution;
using Explorer.Tours.API.Public.Authoring;

namespace Explorer.API.Controllers.Administrator.Execution
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/problem")]
    public class TourProblemController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly ITourService _tourService;
        private readonly INotificationService _notificationService;

        public TourProblemController(ITourProblemService tourProblemService, ITourService tourService, INotificationService notificationService)
        {
            _tourProblemService = tourProblemService;
            _tourService = tourService;
            _notificationService = notificationService;
        }


        [HttpPut("{id:int}")]
        public ActionResult<TourPreferencesDto> Update([FromBody] TourProblemDto tourProblem)
        {
            if (tourProblem.Deadline < DateTime.UtcNow)
            {
                var result = _tourProblemService.SetProblemClosed(tourProblem);
                return CreateResponse(result);
            }
            return CreateResponse(Result.Ok("deadline hasn't passed."));
        }

        [HttpGet("getAll")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll()
        {
            var results = _tourProblemService.GetAll();
            return CreateResponse(results);
        }

        [HttpPost("setDeadline")]
        public ActionResult<PagedResult<TourProblemDto>> SetDeadline([FromQuery] int problemId, [FromQuery] DateTime time)
        {
            var problem = _tourProblemService.GetById(problemId);
            var authorId = _tourService.GetById(problem.Value.TourId).Value.AuthorId;
            var result = _tourProblemService.SetDeadline(problemId, time, authorId);
            notifySetDeadline(problem.Value);
            return CreateResponse(result);
        }

        private void notifySetDeadline(TourProblemDto tourProblemDto)
        {
            string tourName = _tourService.GetById(tourProblemDto.TourId).Value.Name;
            int tourAuthorId = _tourService.GetById(tourProblemDto.TourId).Value.AuthorId;
            string content = $"Deadline has been set on your report of a tour {tourName}!";
            _notificationService.Create(new NotificationDto(content, NotificationType.TourProblemComment, tourProblemDto.Id, tourAuthorId, false));
        }

        [HttpGet("byId")]
        public ActionResult<PagedResult<TourProblemDto>> GetById([FromQuery] int id)
        {
            var result = _tourProblemService.GetById(id);
            return CreateResponse(result);
        }


        [HttpPost("addComment")]
        public ActionResult<PagedResult<TourProblemDto>> AddComment([FromQuery] int tourProblemId, [FromBody] ProblemCommentDto commentDto)
        {
            var result = _tourProblemService.AddComment(tourProblemId, commentDto);
            return CreateResponse(result);
        }
    }
}
