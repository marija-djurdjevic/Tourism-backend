using System;
using System.Collections.Generic;
using Explorer.Stakeholders.Core.Application.Dtos;

namespace Explorer.Stakeholders.Core.Application.Services
{
    public interface IAchievementService
    {
        AchievementDto GetAchievementById(int id);
        IEnumerable<AchievementDto> GetAllAchievements();
        void AddAchievement(AchievementDto achievementDto);
        void AddAchievementToUser(AchievementDtoType type, int userId, int countedCriteriaForUser);
        void UpdateAchievement(int id, AchievementDto achievementDto);
        void DeleteAchievement(int id);
    }
}
