using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IBlogService
    {
        Result<PagedResult<BlogDto>> GetPaged(int page, int pageSize);
        Result<BlogDto> GetBlogById(int id);
        Result<BlogDto> Create(BlogDto blog);
        Result<BlogDto> Update(BlogDto blog);
        Result<BlogDto> AddVote(int blogId, VoteDto vote);
        Result<BlogDto> RemoveVote(int blogId, int authroId);
        Result<List<VoteDto>> GetAllVotesByBlogId(int blogId);
        Result<CommentDto> AddComment(int blogId, CommentDto comment);
        Result<CommentDto> UpdateComment(int blogId, CommentDto comment);
        Result RemoveComment(int blogId, int commentId);
        Result<List<CommentDto>> GetAllCommentsByBlogId(int blogId);
        Result<List<BlogDto>> GetTop3BLogs();
        Result Delete(int id);
    }
}
