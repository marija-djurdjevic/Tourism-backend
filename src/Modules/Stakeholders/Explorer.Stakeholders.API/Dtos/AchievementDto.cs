using Explorer.Stakeholders.API.Dtos;
using System;

namespace Explorer.Stakeholders.Core.Application.Dtos
{
    public class AchievementDto
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int XpReward { get; set; }
        public AchievementDtoType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Criteria { get; private set; }
        public bool isEarnedByMe { get; set; } = false;
        public AchievementDto() { }

        public AchievementDto(
            int id,
            string imagePath,
            int xpReward,
            AchievementDtoType type,
            DateTime earnedOn,
            string name,
            string description,
            string status)
        {
            Id = id;
            ImagePath = imagePath;
            XpReward = xpReward;
            Type = type;
            Name = name;
            Description = description;
            Status = status;
        }

        public override bool Equals(object? obj)
        {
            return obj is AchievementDto dto &&
                   Id == dto.Id;
        }
    }

    public enum AchievementDtoType
    {
        ReviewCreated,
        PhotosInReview,
        SocialEncounters,
        SecretPlacesFound,
        ChallengesCompleted,
        TourCompleted,
        EncountersCreated,
        PointsEarned,
    }
}
