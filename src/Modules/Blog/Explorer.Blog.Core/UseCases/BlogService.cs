using Explorer.Blog.Core.Domain;
using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Public;
using FluentResults;
using AutoMapper;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogDto, Blogs>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public BlogService(IBlogRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _blogRepository = repository;
            _mapper = mapper;
        }

        public Result<BlogDto> AddVote(int blogId, VoteDto voteDto)
        {
            var blog = _blogRepository.Get(blogId);
            if (blog == null)
                return Result.Fail("Blog not found.");

            blog.AddVote(_mapper.Map<VoteDto, Vote>(voteDto));

            _blogRepository.Update(blog);

            var resultDto = MapToDto(blog);
            return Result.Ok(resultDto);
        }
    }
}
