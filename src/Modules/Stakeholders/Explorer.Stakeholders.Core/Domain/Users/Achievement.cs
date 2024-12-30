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
        public string Name { get; private set; }
        public string Description { get; private set; }
        public AchievementStatus Status { get; private set; }
        public int Criteria { get; private set; }

        public ICollection<User> Users { get; private set; }

        public Achievement() { }



        [JsonConstructor]
        public Achievement(int id, string imagePath,int xpReward,AchievementType type,string name,string description,AchievementStatus status)
        {

            Id = id;
            ImagePath = imagePath;
            XpReward = xpReward;
            Type = type;
            Name = name;
            Description = description;
            Status = status;
        }

        protected override bool EqualsCore(Achievement other)
        {
            if (other == null) return false;
            return Id == other.Id;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Id, ImagePath, XpReward, Type, Criteria, Name, Description, Status);
        }
    }

    public enum AchievementType
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

    public enum AchievementStatus
    {
        Active,
        Achieved
    }
}
