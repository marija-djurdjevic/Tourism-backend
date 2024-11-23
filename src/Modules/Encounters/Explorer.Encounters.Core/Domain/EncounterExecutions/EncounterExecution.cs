using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.EncounterExecutions
{
    public class EncounterExecution : Entity
    {
        public int EncounterId { get; private set; }
        public int TouristId { get; private set; }
        public DateTime? CompletedTime { get; private set; }

        public EncounterExecution(int encounterId, int touristId, DateTime? completedTime)
        { 
            EncounterId = encounterId;
            TouristId = touristId;
            CompletedTime = completedTime;
        }
    }
}
