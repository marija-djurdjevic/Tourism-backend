using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public enum EncounterStatus
    {
        Draft,
        Active,
        Archived
    }
    public enum EncounterType
    {
        Social,
        Location,
        Misc
    }
    public class EncounterDto
    {
        public int Id { get; set; }
        public int AdministratorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Xp { get; set; }
        public CoordinatesDto Coordinates { get; set; }
        public EncounterStatus Status { get; set; }
        public EncounterType Type { get;set; }

    }
}
