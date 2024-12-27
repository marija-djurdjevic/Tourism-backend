using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos.SecretsDtos
{
    public class StoryUnlockedDto
    {
        public int UserId { get; set; }

        public int StoryId { get; set; }

        public DateTime UnlockDate { get; set; }
    }
}
