using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.UseCases.Execution;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ITourSessionService tourSessionService;
        public EncounterController(IEncounterService encounterService,ITourSessionService tourSessionService)
        {
            _encounterService = encounterService;
            this.tourSessionService = tourSessionService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EncounterDto>> GetAll()
        {
            var result = _encounterService.GetPaged(0, 0);
            return CreateResponse(result);
        }

        [HttpGet("{tourId}")]
        public ActionResult<List<EncounterDto>> GetAllForUserAndTour(int tourId)
        {
            int userId = User.PersonId();
            Result<int> keyPointId = tourSessionService.GetMostRecentlyCompletedKeyPointId(tourId,userId);
            if (keyPointId.IsFailed)
            {
                return CreateResponse(keyPointId);
            }

            var result = _encounterService.GetPagedForUserAndTour(userId, keyPointId.Value);

            return CreateResponse(result);
        }
    }
}
