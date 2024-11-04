using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourProblems;

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
        CreateMap<KeyPointDto, KeyPoint>().ReverseMap();
        CreateMap<ObjectDto, Domain.Object>().ReverseMap();
        CreateMap<TourPreferencesDto, TourPreferences>().ReverseMap();

        CreateMap<TourProblemDto, TourProblem>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details)).ReverseMap();
        CreateMap<NotificationDto, Notification>().IncludeAllDerived().ReverseMap();
        CreateMap<ProblemDetailsDto, ProblemDetails>().IncludeAllDerived().ReverseMap();
        CreateMap<ProblemCommentDto, ProblemComment>().IncludeAllDerived().ReverseMap();
    }
}