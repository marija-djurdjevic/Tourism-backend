using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos.EncounterExecutionDtos
{
    public class EncounterExecutionDto
    {
        public int Id { get; private set; }
        public int EncounterId { get; private set; }
        public int TouristId { get; private set; }
        public DateTime? CompletedTime { get; private set; }
    }
}
