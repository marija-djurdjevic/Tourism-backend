using System;

namespace Explorer.Stakeholders.Core.Application.DTOs
{
    public class AchievementDto
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int XpReward { get; set; }
        public string Type { get; set; }
        public DateTime EarnedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public AchievementDto() { }

        public AchievementDto(
            int id,
            string imagePath,
            int xpReward,
            string type,
            DateTime earnedOn,
            string name,
            string description,
            string status)
        {
            Id = id;
            ImagePath = imagePath;
            XpReward = xpReward;
            Type = type;
            EarnedOn = earnedOn;
            Name = name;
            Description = description;
            Status = status;
        }
    }
}
