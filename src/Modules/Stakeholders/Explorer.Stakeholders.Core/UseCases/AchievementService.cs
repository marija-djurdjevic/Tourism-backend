using AutoMapper;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Application.Dtos;
using Explorer.Stakeholders.Core.Application.Services;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AchievementService : IAchievementService,IAchievementInternalService
    {
        private readonly IUserService _userService;
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public AchievementService(IAchievementRepository achievementRepository,IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public void AddAchievement(AchievementDto achievementDto)
        {
            var achievement = _mapper.Map<Achievement>(achievementDto);
            _achievementRepository.Add(achievement);
        }
        public void AddAchievementToUser(AchievementDtoType type,int userId, int countedCriteriaForUser)
        {
            var user = _userService.GetUserById(userId);
            foreach(var achi in GetAllAchievements())
            {
                if(achi.Criteria<=countedCriteriaForUser && achi.Type==type && !user.Value.Achievements.Contains(achi))
                {
                    user.Value.Achievements.Add(achi);
                    _userService.UpdateAchievements(user.Value);
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
