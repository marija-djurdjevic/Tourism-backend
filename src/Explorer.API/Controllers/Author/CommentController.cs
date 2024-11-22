using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    //[Authorize(Policy = "authorPolicy")]
    //[Route("api/author/blog/{blogId:int}/comment")]
    //public class CommentController : BaseApiController
    //{
    //    private readonly ICommentService _commentService;

    //    public CommentController(ICommentService commentService)
    //    {
    //        _commentService = commentService;
    //    }

    //    //[HttpGet]
    //    //public ActionResult<PagedResult<CommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    //    //{
    //    //    var result = _commentService.GetPaged(page, pageSize);
    //    //    return CreateResponse(result);
    //    //}

    //    [HttpGet]
    //    public ActionResult<List<CommentDto>> GetAllByBlogId(int blogId)
    //    {
    //        var result = _commentService.GetByBlogId(blogId);
    //        var pagedResult = new PagedResult<CommentDto>(result.Value, result.Value.Count).ToResult();
    //        return CreateResponse(pagedResult);
    //    }

    //    [HttpPost]
    //    public ActionResult<CommentDto> Create([FromBody] CommentDto comment)
    //    {
    //        var result = _commentService.Create(comment);
    //        return CreateResponse(result);
    //    }

    //    [HttpPut("{id:int}")]
    //    public ActionResult<CommentDto> Update([FromBody] CommentDto comment)
    //    {
    //        var result = _commentService.Update(comment);
    //        return CreateResponse(result);
    //    }

    //    [HttpDelete("{id:int}")]
    //    public ActionResult Delete(int id)
    //    {
    //        var result = _commentService.Delete(id);
    //        return CreateResponse(result);
    //    }
    //}
}
