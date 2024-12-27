using AutoMapper;
using Explorer.Encounters.API.Dtos.EncounterDtos;
using Explorer.Encounters.API.Dtos.EncounterExecutionDtos;
using Explorer.Encounters.API.Dtos.SecretsDtos;
using Explorer.Encounters.Core.Domain.EncounterExecutions;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Mappers
{
    public class EncountersProfile : Profile
    {
        public EncountersProfile() 
        {
            
            CreateMap<EncounterDto, Encounter>()
           .ForMember(dest => dest.Coordinates, opt => opt.MapFrom(src => src.Coordinates)).ReverseMap();
            CreateMap<Coordinates, CoordinatesDto>().ReverseMap();


            CreateMap<EncounterExecutionDto, EncounterExecution>().ReverseMap();

            CreateMap<BookDto,Book>().ReverseMap();
            CreateMap<StoryDto, Story>().ReverseMap();
            CreateMap<StoryUnlockedDto, StoryUnlocked>().ReverseMap();
        }
    }
}
