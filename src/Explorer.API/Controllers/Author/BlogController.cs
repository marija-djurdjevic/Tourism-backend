﻿using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
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

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.GetPaged(page, pageSize);
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
    }
}
