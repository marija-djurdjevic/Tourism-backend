using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentDto, Comment>, ICommentService
    {
        private readonly ICommentRepository commentRepository;
        public CommentService(ICommentRepository commentRepository, IMapper mapper) : base(commentRepository, mapper)
        {
            this.commentRepository = commentRepository;
        }

        public Result<List<CommentDto>> GetByBlogId(int blogId)
        {
            return MapToDto(commentRepository.GetByBlogId(blogId));
        }

        public Result DeleteByBlogId(int blogId)
        {
            try
            {
                List<CommentDto> comments = GetByBlogId(blogId).Value;
                foreach (CommentDto comment in comments)
                {
                    commentRepository.Delete(comment.Id);
                }

                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<CommentDto>> GetLatestComments()
        {
            return commentRepository.GetLatestComments();
        }
    }
}
