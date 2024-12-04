using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.PublishRequestDtos;

using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.PublishRequests;

using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.TourSessions;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap();
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        //CreateMap<KeyPointDto, KeyPoint>().ReverseMap();
        CreateMap<ObjectDto, Domain.Object>().ReverseMap();
        CreateMap<TourPreferencesDto, TourPreferences>().ReverseMap();
        CreateMap<CoordinateDto, Coordinates>().ReverseMap();


        CreateMap<TourSession, TourSessionDto>()
            .ForMember(dest => dest.CurrentLocation, opt => opt.MapFrom(src => src.CurrentLocation))
            .ForMember(dest => dest.CompletedKeyPoints, opt => opt.MapFrom(src => src.CompletedKeyPoints))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.LastActivity, opt => opt.MapFrom(src => src.LastActivity));


        CreateMap<TourDto, Tour>().ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt.ToUniversalTime()));
        CreateMap<TourDto, Tour>()
            .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt.ToUniversalTime()))
            .ForMember(dest => dest.TransportInfo, opt => opt.MapFrom(src => src.TransportInfo)).ReverseMap();
        CreateMap<TourSessionDto, TourSession > ()
        .ForMember(dest => dest.CurrentLocation, opt => opt.MapFrom(src => src.CurrentLocation))
        .ForMember(dest => dest.CompletedKeyPoints, opt => opt.MapFrom(src => src.CompletedKeyPoints));
        CreateMap<CompletedKeyPointDto, CompletedKeyPoints>().IncludeAllDerived().ReverseMap();
        CreateMap<LocationDto, Location>().IncludeAllDerived().ReverseMap();
        CreateMap<TransportInfo, TransportInfoDto>().ReverseMap();
        CreateMap<KeyPoint, KeyPointDto>()
           .ForMember(dto => dto.Longitude, opt => opt.MapFrom(src => src.Coordinates.Longitude))
           .ForMember(dto => dto.Latitude, opt => opt.MapFrom(src => src.Coordinates.Latitude))
           .ReverseMap()
           .ForPath(src => src.Coordinates, opt => opt.MapFrom(dto => new Coordinates(dto.Latitude, dto.Longitude)));

        
        CreateMap<TourProblemDto, TourProblem>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details)).ReverseMap();
        CreateMap<NotificationDto, Notification>().IncludeAllDerived().ReverseMap();
        CreateMap<ProblemDetailsDto, ProblemDetails>().IncludeAllDerived().ReverseMap();
        CreateMap<ProblemCommentDto, ProblemComment>().IncludeAllDerived().ReverseMap();
        CreateMap<PublishRequestDto, PublishRequest>()
        .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment ?? string.Empty))
        .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
        .ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdminId))
        .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
        .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
        .ReverseMap();
    }
}