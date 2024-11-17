using AutoMapper;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain.Encounters;
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
        }
    }
}
