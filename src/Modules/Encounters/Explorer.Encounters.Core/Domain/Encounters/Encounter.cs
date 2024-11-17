using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.Encounters
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
    public class Encounter : Entity
    {
        public int AuthorId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }   
        public int Xp { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public EncounterStatus Status { get; private set; }
        public EncounterType Type { get; private set; }

        public Encounter() { }
        public Encounter(int authorId, string name, string description, int xp, Coordinates coordinates, EncounterStatus status, EncounterType type)
        {
            
            AuthorId = authorId;
            Name = name;
            Description = description;
            Xp = xp;
            Coordinates = coordinates;
            Status = status;
            Type = type;
        }

    }

}
