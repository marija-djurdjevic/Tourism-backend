using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.ShoppingCarts;
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
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<ClubDto, Club>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<TourProblemDto, TourProblem>().ReverseMap();
        //CreateMap<KeyPointDto, KeyPoint>().ReverseMap();
        CreateMap<ObjectDto, Domain.Object>().ReverseMap();
        CreateMap<TourPreferencesDto, TourPreferences>().ReverseMap();


        CreateMap<TourSessionDto, TourSession > ()
        //.ForMember(dest => dest.CurrentLocation, opt => opt.MapFrom(src => src.CurrentLocation))
        .ForMember(dest => dest.CompletedKeyPoints, opt => opt.MapFrom(src => src.CompletedKeyPoints));
    
        CreateMap<CompletedKeyPointDto, CompletedKeyPoints>().IncludeAllDerived().ReverseMap();
        CreateMap<LocationDto, Location>().IncludeAllDerived().ReverseMap();

        CreateMap<KeyPoint, KeyPointDto>()
           .ForMember(dto => dto.Longitude, opt => opt.MapFrom(src => src.Coordinates.Longitude))
           .ForMember(dto => dto.Latitude, opt => opt.MapFrom(src => src.Coordinates.Latitude))
           .ReverseMap()
           .ForPath(src => src.Coordinates, opt => opt.MapFrom(dto => new Coordinates(dto.Latitude, dto.Longitude)));

        CreateMap<ShoppingCartDto, ShoppingCart>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.Tokens, opt => opt.MapFrom(src => src.Tokens)).ReverseMap();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();
    }
}