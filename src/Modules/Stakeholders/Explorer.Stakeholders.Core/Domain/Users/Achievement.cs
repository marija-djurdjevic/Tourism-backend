using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Users
{
    public class Achievement : ValueObject<Achievement>
    {
        public int Id { get; private set; }
        public string ImagePath { get; private set; }
        public int XpReward { get; private set; }
        public AchievementType Type { get; private set; }
        public DateTime EarnedOn { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AchievementStatus Status { get; private set; }



        public Achievement() { }



        [JsonConstructor]
        public Achievement(int id, string imagePath,int xpReward,AchievementType type,DateTime earnedOn,string name,string description,AchievementStatus status)
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

        protected override bool EqualsCore(Achievement other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   ImagePath == other.ImagePath &&
                   XpReward == other.XpReward &&
                   Type == other.Type &&
                   EarnedOn == other.EarnedOn &&
                   Name == other.Name &&
                   Description == other.Description &&
                   Status == other.Status;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Id, ImagePath, XpReward, Type, EarnedOn, Name, Description, Status);
        }
    }

    public enum AchievementType
    {
        Tour,
        Session
    }

    public enum AchievementStatus
    {
        Active,
        Achieved
    }
}
