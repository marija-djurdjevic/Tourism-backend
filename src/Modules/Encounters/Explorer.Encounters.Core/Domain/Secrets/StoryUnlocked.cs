using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Secrets
{
    public class StoryUnlocked : Entity
    {
        public int UserId { get; set; }

        public int StoryId { get; set; }

        public DateTime UnlockDate { get; set; }

        public StoryUnlocked() 
        {
            UnlockDate = DateTime.UtcNow;
        }
        public StoryUnlocked(int userId, int storyId, DateTime unlockDate)
        {
            UserId = userId;
            StoryId = storyId;
            UnlockDate = unlockDate;
        }
    }
}
