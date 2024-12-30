using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public.Administration;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Application.Services;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Application.Dtos;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/achievement")]
    public class AchievementController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly IAchievementService achievementService;

        public AchievementController(IUserService userService,IAchievementService achievementService)
        {
            this.userService = userService;
            this.achievementService = achievementService;
        }

        [HttpGet]
        public ActionResult<PagedResult<AchievementDto>> getAchievements()
        {

            int userId;
            try
            {
                userId = User.PersonId();
            }
            catch (Exception)
            {
                userId = -2; // Postavi podrazumevanu vrednost u slučaju greške
            }
            
            var user = userService.GetUserById(userId);
            var achievements = achievementService.GetAllAchievements();
            foreach(var achievement in achievements)
            {
                if (user.Value.Achievements.Contains(achievement)){
                    achievement.isEarnedByMe = true;
                }
                else
                {
                    achievement.ImagePath = "assets/badge.png";
                }
            }
            return Ok(achievements);
        }
    }
}
