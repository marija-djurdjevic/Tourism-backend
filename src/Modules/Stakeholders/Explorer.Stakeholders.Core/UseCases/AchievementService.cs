using AutoMapper;
using Explorer.Stakeholders.Core.Application.DTOs;
using Explorer.Stakeholders.Core.Application.Services;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;

        public AchievementService(IAchievementRepository achievementRepository, IMapper mapper)
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
        }

        public void AddAchievement(AchievementDto achievementDto)
        {
            
            var achievement = _mapper.Map<Achievement>(achievementDto);
            _achievementRepository.Add(achievement);
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
