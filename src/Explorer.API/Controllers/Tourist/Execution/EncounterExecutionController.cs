using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Dtos.EncounterExecutionDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EncounterType = Explorer.Encounters.API.Dtos.EncounterDtos.EncounterType;

namespace Explorer.API.Controllers.Tourist.Execution
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounterExecution")]
    public class EncounterExecutionController : BaseApiController
    {
        private readonly IEncounterAchievementService _encounterAchievementService;
        private readonly IEncounterExecutionService _encounterExecutionService;
        private readonly IEncounterService _encounterService;
        private readonly IUserService userService;

        public EncounterExecutionController(IEncounterAchievementService encounterAchievementService, IEncounterExecutionService encounterExecutionService, IEncounterService encounterService, IUserService userService)
        {
            _encounterAchievementService = encounterAchievementService;
            _encounterExecutionService = encounterExecutionService;
            _encounterService = encounterService;
            this.userService = userService;
        }

        [HttpPost("create")]
        public ActionResult<EncounterExecutionDto> Create([FromBody] EncounterExecutionDto encounterExecutionDto)
        {
            var userId = User.PersonId();
            encounterExecutionDto.TouristId = userId;
            var encounter = _encounterService.Get(encounterExecutionDto.EncounterId);
            if (encounter.IsFailed)
            {
                return CreateResponse(encounter);
            }

            // Check if the encounter execution already exists
            var encounterExecutions = _encounterExecutionService.GetPaged(0, 0);
            var encounterExecution = encounterExecutions.Value.Results
                .Find(execution =>
                    execution.TouristId == encounterExecutionDto.TouristId &&
                    execution.EncounterId == encounterExecutionDto.EncounterId);

            // If it doesn't exist, create it
            if (encounterExecution == null)
            {
                var result = _encounterExecutionService.Create(encounterExecutionDto);
                if (result.IsSuccess)
                {
                    userService.UpdateXPs(userId, encounter.Value.Xp);
                }
                _encounterAchievementService.CheckForAchievements(userId);
                return CreateResponse(result);
            }

            return BadRequest(new { Error = "Encounter Execution already exists" });
        }


        [HttpPost("update")]
        public ActionResult<EncounterExecutionDto> Update([FromBody] EncounterExecutionDto encounterExecutionDto)
        {
            var encounter = _encounterService.Get(encounterExecutionDto.EncounterId);

            if (encounter.Value.Type == EncounterType.Social)
            {
                var socialEncounterExecutions = _encounterExecutionService.GetPaged(0, 0)
                    .Value.Results.FindAll(execution =>
                        execution.EncounterId == encounterExecutionDto.EncounterId &&
                        execution.CompletedTime == null);

                if (socialEncounterExecutions.Count >= encounter.Value.TouristNumber)
                {
                    foreach (var socialEncounterExecution in socialEncounterExecutions)
                    {
                        // Update the CompletedTime and save the changes
                        _encounterExecutionService.Update(socialEncounterExecution.Id);
                        _encounterAchievementService.CheckForAchievements(socialEncounterExecution.TouristId);
                    }
                    return Ok("Operation successful");
                }
            }

            return BadRequest(new { Error = "Encounter update failed or conditions not met" });
        }
    }
};
