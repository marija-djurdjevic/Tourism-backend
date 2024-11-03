using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<CommentDto, Comment>().ReverseMap();
        CreateMap<VoteDto, Vote>().ReverseMap();

        CreateMap<BlogDto, Blogs>()
           .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes))
           .ReverseMap();
    }
}