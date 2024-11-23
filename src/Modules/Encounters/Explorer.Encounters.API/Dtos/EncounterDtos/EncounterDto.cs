using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos.EncounterDtos
{
    public enum EncounterStatus
    {
        Draft,
        Active,
        Archived
    }
    public enum EncounterCreator
    {
        AuthorRequired,
        Author,
        Tourist
    }
    public enum EncounterType
    {
        Social,
        Location,
        Misc
    }
    public class EncounterDto
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int KeyPointId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Xp { get; private set; }
        public CoordinatesDto Coordinates { get; private set; }
        public EncounterStatus Status { get; private set; }
        public EncounterType Type { get; private set; }
        public EncounterCreator Creator { get; private set; }

        public int? Range { get; private set; }
        public int? TouristNumber { get; private set; }
        public string? ImagePath { get; private set; }
    }
}
