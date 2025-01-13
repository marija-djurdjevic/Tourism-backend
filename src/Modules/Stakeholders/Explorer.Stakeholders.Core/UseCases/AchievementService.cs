using AutoMapper;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Application.Dtos;
using Explorer.Stakeholders.Core.Application.Services;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AchievementService : IAchievementService, IAchievementInternalService
    {
        private readonly IUserService _userService;
        private readonly INotificationInternalService _notificationInternalService;
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public AchievementService(IAchievementRepository achievementRepository, IUserService userService, INotificationInternalService notificationInternalService, IMapper mapper)
        {
            this._userService = userService;
            this._notificationInternalService = notificationInternalService;
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public void AddAchievement(AchievementDto achievementDto)
        {
            var achievement = _mapper.Map<Achievement>(achievementDto);
            _achievementRepository.Add(achievement);
        }
        public void AddAchievementToUser(AchievementDtoType type, int userId, int countedCriteriaForUser)
        {
            var user = _userService.GetUserById(userId);
            foreach (var achi in GetAllAchievements())
            {
                if (user.Value.Achievements != null && achi.Criteria <= countedCriteriaForUser && achi.Type == type && !user.Value.Achievements.Contains(achi))
                {
                    user.Value.Achievements.Add(achi);
                    _userService.UpdateAchievements(user.Value);
                    _userService.UpdateXPs(user.Value.Id, achi.XpReward);
                    user = _userService.GetUserById(userId);

                    var notification = new NotificationDto($"You have achieved {achi.Name} achievement", NotificationType.Achievement, 0, userId, false);
                    notification.ImagePath = achi.ImagePath;
                    _notificationInternalService.Create(notification);
                    _notificationInternalService.NotifyUserAsync(userId, notification);

                    if (user.IsSuccess && user.Value.XP!=null)
                        AddAchievementToUser(Stakeholders.Core.Application.Dtos.AchievementDtoType.PointsEarned, userId, (int)user.Value.XP);
                
                }
            }
        }

        public void DeleteAchievement(int id)
        {
            var achievement = _achievementRepository.GetById(id);

            _achievementRepository.Delete(id);
        }

        public AchievementDto GetAchievementById(int id)
        {
            var achievement = _achievementRepository.GetById(id);

            return _mapper.Map<AchievementDto>(achievement);
        }



        public IEnumerable<AchievementDto> GetAllAchievements()
        {
            var achievements = _achievementRepository.GetAll();
            return achievements.Select(_mapper.Map<AchievementDto>).ToList();
        }

        public void UpdateAchievement(int id, AchievementDto achievementDto)
        {
            if (achievementDto == null)
                throw new ArgumentNullException(nameof(achievementDto), "AchievementDto cannot be null.");

            var existingAchievement = _achievementRepository.GetById(id);


            var updatedAchievement = _mapper.Map(achievementDto, existingAchievement);
            _achievementRepository.Update(updatedAchievement);
        }
    }
}
