using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Dtos.EncounterExecutionDtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class EncounterAchievementService:IEncounterAchievementService
    {
        public readonly IEncounterService _encounterService;
        public readonly IEncounterExecutionService _encounterExecutionService;
        public readonly IAchievementInternalService _achievementInternalService;

        public EncounterAchievementService(IEncounterService encounterService, IEncounterExecutionService encounterExecutionService, IAchievementInternalService achievementInternalService)
        {
            this._encounterService = encounterService;
            this._encounterExecutionService = encounterExecutionService;
            this._achievementInternalService = achievementInternalService;
        }

        public void CheckForAchievements(int userId)
        {
            var myEncounterExecutions = _encounterExecutionService.GetPaged(0, 0).Value.Results
                .Where(e => e.TouristId == userId && e.CompletedTime != null);
            var allEncounters = _encounterService.GetPaged(0, 0).Value.Results
                .Where(e => e.Status == API.Dtos.EncounterDtos.EncounterStatus.Active);

            var numberOfMyLocationEncounters = myEncounterExecutions.Count(e => allEncounters.Any(x => x.Id == e.EncounterId && x.Type == API.Dtos.EncounterDtos.EncounterType.Location));
            var numberOfMySocialEncounters = myEncounterExecutions.Count(e => allEncounters.Any(x => x.Id == e.EncounterId && x.Type == API.Dtos.EncounterDtos.EncounterType.Social));
            var numberOfMyEncounters = myEncounterExecutions.Count();

            _achievementInternalService.AddAchievementToUser(AchievementDtoType.SecretPlacesFound, userId, numberOfMyLocationEncounters);
            _achievementInternalService.AddAchievementToUser(AchievementDtoType.SocialEncounters, userId, numberOfMySocialEncounters);
            _achievementInternalService.AddAchievementToUser(AchievementDtoType.ChallengesCompleted, userId, numberOfMyEncounters);
        }

        public void CheckForAchievementsForCreatedEnclounters(int userId)
        {
            var allEncounters = _encounterService.GetPaged(0, 0).Value.Results
                .Where(e => e.UserId == userId);

            var numberOfMyEncounters = allEncounters.Count();

            _achievementInternalService.AddAchievementToUser(AchievementDtoType.EncountersCreated, userId, numberOfMyEncounters);
        }
    }
}
