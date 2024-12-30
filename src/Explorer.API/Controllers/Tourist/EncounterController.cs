using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Public.Authoring;
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
        private readonly IEncounterAchievementService encounterAchievementService;
        private readonly IEncounterService _encounterService;
        private readonly ITourSessionService tourSessionService;
        private readonly IUserService userService;
        private readonly IKeyPointService _keyPointService;
        public EncounterController(IEncounterAchievementService encounterAchievementService,IEncounterService encounterService,ITourSessionService tourSessionServic,IUserService userService,IKeyPointService keyPointService)
        {
            this.encounterAchievementService = encounterAchievementService;
            _encounterService = encounterService;
            this.tourSessionService = tourSessionServic;
            this.userService = userService;
            _keyPointService = keyPointService;
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

        [HttpPost]
        public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounter)
        {
            int userId = User.PersonId();
            encounter.UserId = userId;

            if(encounter.Type != EncounterType.Location)
            {
                encounter.Coordinates.Latitude = _keyPointService.Get(encounter.KeyPointId).Value.Latitude;
                encounter.Coordinates.Longitude = _keyPointService.Get(encounter.KeyPointId).Value.Longitude;
            }
            
            encounter.Status = EncounterStatus.Draft;
            encounter.Creator = EncounterCreator.Tourist;
            if (userService.GetLevelById(userId).Value<10)
            {
                var badResult = Result.Fail("User level is too low");

                // Vraćanje 400 sa porukom
                return BadRequest(new
                {
                    error = badResult // Prosljeđuješ poruku iz rezultata
                });
            }
            var result = _encounterService.Create(encounter);
            encounterAchievementService.CheckForAchievementsForCreatedEnclounters(userId);
            return CreateResponse(result);
        }
    }
}
