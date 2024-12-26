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
    public interface ICommentService
    {
        Result<PagedResult<CommentDto>> GetPaged(int page, int pageSize);
        Result<List<CommentDto>> GetByBlogId(int blogId);
        Result<List<CommentDto>> GetLatestComments();
        Result<CommentDto> Create(CommentDto comment);
        Result<CommentDto> Update(CommentDto comment);
        Result Delete(int id);
        Result DeleteByBlogId(int blogId);
    }
}
