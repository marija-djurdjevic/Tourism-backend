using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos.EncounterExecutionDtos
{
    public class EncounterExecutionDto
    {
        public int Id { get; set; }
        public int EncounterId { get; set; }
        public int TouristId { get; set; }
        public DateTime? CompletedTime { get; set; }
    }
}
