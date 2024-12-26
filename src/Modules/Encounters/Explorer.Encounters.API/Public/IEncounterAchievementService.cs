using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IEncounterAchievementService
    {
        void CheckForAchievements(int userId);
        void CheckForAchievementsForCreatedEnclounters(int userId);
    }
}
