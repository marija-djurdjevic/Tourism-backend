using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;

        public BlogController(IBlogService blogService, IUserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}")]
        public ActionResult<BlogDto> GetBlogById(int blogId)
        {
            var result = _blogService.GetBlogById(blogId);
            return CreateResponse(result);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<UserDto> GetUserById(int userId)
        {
            var result = _userService.GetUserById(userId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BlogDto> Create([FromBody] BlogDto blog)
        {
            System.Diagnostics.Debug.WriteLine(blog);
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [HttpPost("{blogId}/vote")]
        public ActionResult<BlogDto> AddVote(int blogId, [FromBody] VoteDto vote)
        {
            var result = _blogService.AddVote(blogId, vote);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}/votes")]
        public ActionResult<BlogDto> GetAllVotesByBlogId(int blogId)
        {
            var result = _blogService.GetAllVotesByBlogId(blogId);
            return CreateResponse(result);
        }

        [HttpDelete("{blogId}/{authorId}/votes")]
        public ActionResult<BlogDto> RemoveVote(int blogId, int authorId)
        {
            var result = _blogService.RemoveVote(blogId, authorId);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BlogDto> Update([FromBody] BlogDto blog)
        {
            var result = _blogService.Update(blog);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _blogService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}/comments")]
        public ActionResult<List<CommentDto>> GetComments(int blogId)
        {
            var result = _blogService.GetAllCommentsByBlogId(blogId);
            return CreateResponse(result);
        }

        [HttpPost("{blogId}/comments")]
        public ActionResult<CommentDto> AddComment(int blogId, [FromBody] CommentDto comment)
        {
            var result = _blogService.AddComment(blogId, comment);
            return CreateResponse(result);
        }

        [HttpPut("{blogId}/comments/{commentId}")]
        public ActionResult<CommentDto> UpdateComment(int blogId, [FromBody] CommentDto comment)
        {
            var result = _blogService.UpdateComment(blogId, comment);
            return CreateResponse(result);
        }

        [HttpDelete("{blogId}/comments/{commentId}")]
        public ActionResult<CommentDto> RemoveComment(int blogId, int commentId)
        {
            var result = _blogService.RemoveComment(blogId, commentId);
            return CreateResponse(result);
        }
    }
}
