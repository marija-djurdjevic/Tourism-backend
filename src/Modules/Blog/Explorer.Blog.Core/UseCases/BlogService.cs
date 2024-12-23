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
        private readonly ICommentService _commentService;
        private readonly IMailService _mailService;
        public BlogService(IBlogRepository repository, ICommentService commentService, IMailService mailService, IMapper mapper) : base(repository, mapper)
        {
            _blogRepository = repository;
            _mapper = mapper;
            _commentService = commentService;
            _mailService = mailService;
        }

        public Result<BlogDto> GetBlogById(int blogId)
        {
            return MapToDto(_blogRepository.GetBlogById(blogId));
        }

        public Result<BlogDto> AddVote(int blogId, VoteDto voteDto)
        {
            try
            {
                var blog = _blogRepository.Get(blogId);
                if (blog == null)
                    return Result.Fail("Blog not found.");

                var isClosed = CheckIfClosed(blog);
                if (isClosed.IsFailed)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Blog closed.");
                }


                blog.AddVote(_mapper.Map<VoteDto, Vote>(voteDto));

                _blogRepository.Update(blog);

                var resultDto = MapToDto(blog);

                UpdateStatus(blogId);

                MessageDto mail = new MessageDto("zekovicognjen753@gmail.com",
                    "pswtesttest@gmail.com",
                    "imqtapmbwgkjsmww",
                    "Naslov",
                    "<b>Boooooldovani<b>");
                _mailService.SendEmail(mail);
                //davon.will7@ethereal.email
                //AUsRG4upyPMJRgHgaB
                //clotilde70@ethereal.email
                //KvardNpSsCTQfwTGMm

                return Result.Ok(resultDto);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        private Result<bool> CheckIfClosed(Blogs blog)
        {
            bool isClosed = blog.Status == BlogStatus.Closed;
            if (isClosed)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Blog not editable");
            }

            return Result.Ok(isClosed);
        }

        public Result<BlogDto> RemoveVote(int blogId, int authorId)
        {
            try
            {
                var blog = _blogRepository.Get(blogId);
                if (blog == null)
                    return Result.Fail("Blog not found.");

                var isClosed = CheckIfClosed(blog);
                if (isClosed.IsFailed)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Blog closed.");
                }

                blog.RemoveVote(authorId);

                _blogRepository.Update(blog);

                var resultDto = MapToDto(blog);

                UpdateStatus(blogId);

                return Result.Ok(resultDto);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<VoteDto>> GetAllVotesByBlogId(int blogId)
        {
            var blog = _blogRepository.Get(blogId);
            if (blog == null)
                return Result.Fail<List<VoteDto>>("Blog not found.");

            var votesDto = blog.Votes?.Select(vote => _mapper.Map<Vote, VoteDto>(vote)).ToList() ?? new List<VoteDto>();

            return Result.Ok(votesDto);
        }

        private Result UpdateStatus(int blogId)
        {
            var blog = _blogRepository.Get(blogId);
            int badVotes = 0;
            int goodVotes = 0;
            foreach (Vote vote in blog.Votes)
            {
                if (vote.Value == false) { badVotes++; }
                else { goodVotes++; }
            }
            int votes = goodVotes - badVotes;
            var comments = _commentService.GetByBlogId(blogId).Value.Count;
            bool isActive = (votes > 100 && votes < 500) || (comments > 10 && comments <= 30);
            bool isFamous = votes > 500 || comments > 30;

            if (votes < -10) { blog.Status = BlogStatus.Closed; }
            else if (isActive) { blog.Status = BlogStatus.Active; }
            else if (isFamous) { blog.Status = BlogStatus.Famous; }

            _blogRepository.Update(blog);

            return Result.Ok();

        }

        public override Result Delete(int id)
        {
            try
            {
                var blog = _blogRepository.Get(id);
                if (blog == null)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Blog not found.");
                }

                _commentService.DeleteByBlogId(id);

                _blogRepository.Delete(id);

                return Result.Ok();

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<CommentDto>> GetAllCommentsByBlogId(int blogId)
        {
            try
            {
                var resultsDto = _commentService.GetByBlogId(blogId).Value;

                return Result.Ok(resultsDto);

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<CommentDto> AddComment(int blogId, CommentDto commentDto)
        {
            try
            {
                var blog = _blogRepository.Get(blogId);
                if (blog == null)
                    return Result.Fail("Blog not found.");

                var isClosed = CheckIfClosed(blog);
                if (isClosed.IsFailed)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Blog closed.");
                }


                var resultDto = _commentService.Create(commentDto).Value;

                UpdateStatus(blogId);

                return Result.Ok(resultDto);

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<CommentDto> UpdateComment(int blogId, CommentDto commentDto)
        {
            try
            {
                var blog = _blogRepository.Get(blogId);
                if (blog == null)
                    return Result.Fail("Blog not found.");

                var isClosed = CheckIfClosed(blog);
                if (isClosed.IsFailed)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Blog closed.");
                }


                var resultDto = _commentService.Update(commentDto).Value;

                return Result.Ok(resultDto);

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result RemoveComment(int blogId, int commentId)
        {
            try
            {
                var blog = _blogRepository.Get(blogId);
                if (blog == null)
                    return Result.Fail("Blog not found.");

                var isClosed = CheckIfClosed(blog);
                if (isClosed.IsFailed)
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Blog closed.");
                }


                _commentService.Delete(commentId);

                UpdateStatus(blogId);

                return Result.Ok();

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<BlogDto>> GetTop3BLogs()
        {
            var latestCommentsDto = _commentService.GetLatestComments();

            List<Comment> latestComments = new List<Comment>();
            foreach (var comment in latestCommentsDto.Value)
            {
                latestComments.Add(_mapper.Map<CommentDto, Comment>(comment));
            }

            var blog = _blogRepository.GetTop3BLogs(latestComments);

            var blogsDto = blog.Select(blog => _mapper.Map<Blogs, BlogDto>(blog)).ToList() ?? new List<BlogDto>();

            return Result.Ok(blogsDto);
        }
    }
}
