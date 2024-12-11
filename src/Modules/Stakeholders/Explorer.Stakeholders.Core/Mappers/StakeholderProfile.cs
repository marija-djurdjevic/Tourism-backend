using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Application.DTOs;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<AccountReviewDto, User>().ReverseMap();
        CreateMap<UserProfileDto, UserProfile>().ReverseMap();
        CreateMap<UserRatingDto, UserRating>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<LocationDto, Location>().ReverseMap();
        CreateMap<AchievementDto, Achievement>().ReverseMap();
    }
}