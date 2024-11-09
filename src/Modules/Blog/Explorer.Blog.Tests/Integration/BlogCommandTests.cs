using Explorer.API.Controllers.Author;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class BlogCommandTests : BaseBlogIntegrationTest
    {
        public BlogCommandTests(BlogTestFactory factory) : base(factory){}

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
                "NASLOVCINA", 
                new List<VoteDto>
                {
                    new VoteDto { AuthorId = 101, CreationDate = DateTime.UtcNow, Value = true },
                    new VoteDto { AuthorId = 102, CreationDate = DateTime.UtcNow, Value = true }
                }, 
                456, 
                "dadadsdadsdasdasdas", 
                DateTime.UtcNow, 
                "slika.png", 
                1 
            };

            yield return new object[]
            {
                "AnotherTitle", 
                new List<VoteDto>
                {
                    new VoteDto { AuthorId = 201, CreationDate = DateTime.UtcNow, Value = true }
                }, 
                789, 
                "AnotherDescription", 
                DateTime.UtcNow, 
                "anotherImage.png", 
                2 
            };
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Create(string blogTitle, List<VoteDto> votes, int voteAuthorId,string description, DateTime voteCreationDate, string img, int status)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new BlogDto
            {
                Title = blogTitle,
                AuthorId = voteAuthorId,
                Votes = votes,
                Description = description,
                CreationDate = voteCreationDate,
                Image = img,
                Status = BlogDto.BlogStatus.Published,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogDto;

            // Assert - Response

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);
            result.Description.ShouldBe(newEntity.Description);
            result.CreationDate.ShouldBe(newEntity.CreationDate);
            result.Image.ShouldBe(newEntity.Image);
            result.Status.ShouldBe(newEntity.Status);
            result.AuthorId.ShouldBe(newEntity.AuthorId);
            result.Votes.Count.ShouldBe(newEntity.Votes.Count);
            for (int i = 0; i < result.Votes.Count; i++)
            {
                result.Votes[i].AuthorId.ShouldBe(newEntity.Votes[i].AuthorId);
                result.Votes[i].CreationDate.ShouldBe(newEntity.Votes[i].CreationDate);
                result.Votes[i].Value.ShouldBe(newEntity.Votes[i].Value);
            }

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Theory]
        [InlineData("Title1", 456, "2024-11-06T00:00:00", true)]
        public void AddSingleVoteToExistingBlog_ThroughController(string blogTitle, int voteAuthorId, string voteCreationDate, bool voteValue)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var existingBlog = dbContext.Blogs.FirstOrDefault(b => b.Title == blogTitle);
            existingBlog.ShouldNotBeNull();

            var newVote = new VoteDto
            {
                AuthorId = voteAuthorId,
                CreationDate = DateTime.Parse(voteCreationDate),
                Value = voteValue
            };

            // Act
            var addVoteResult = controller.AddVote(Convert.ToInt32(existingBlog.Id), newVote).Result as ObjectResult;
            var updatedBlog = dbContext.Blogs.FirstOrDefault(b => b.Title == blogTitle);

            // Assert - Response and Database
            addVoteResult.ShouldNotBeNull();
            addVoteResult.StatusCode.ShouldBe(200);

            updatedBlog.ShouldNotBeNull();
            updatedBlog.Votes.ShouldContain(v =>
                v.AuthorId == newVote.AuthorId &&
                v.CreationDate == newVote.CreationDate &&
                v.Value == newVote.Value);
        }

        [Theory]
        [InlineData("Title1", 123, "2024-11-06T00:00:00", false)]
        public void UpdateSingleVoteToExistingBlog_ThroughController(string blogTitle, int voteAuthorId, string voteCreationDate, bool voteValue)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var existingBlog = dbContext.Blogs.FirstOrDefault(b => b.Title == blogTitle);
            existingBlog.ShouldNotBeNull();

            var newVote = new VoteDto
            {
                AuthorId = voteAuthorId,
                CreationDate = DateTime.Parse(voteCreationDate),
                Value = voteValue
            };

            // Act
            var updateVoteResult = controller.AddVote(Convert.ToInt32(existingBlog.Id), newVote).Result as ObjectResult;
            var updatedBlog = dbContext.Blogs.FirstOrDefault(b => b.Title == blogTitle);

            // Assert - Response and Database
            updateVoteResult.ShouldNotBeNull();
            updateVoteResult.StatusCode.ShouldBe(200);

            updatedBlog.ShouldNotBeNull();
            updatedBlog.Votes.ShouldContain(v =>
                v.AuthorId == newVote.AuthorId &&
                v.CreationDate == newVote.CreationDate &&
                v.Value == newVote.Value);
        }

        [Theory]
        [InlineData("Title1", 123, "2024-11-06T00:00:00", false)]
        public void DeleteSingleVoteToExistingBlog_ThroughController(string blogTitle, int voteAuthorId, string voteCreationDate, bool voteValue)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var existingBlog = dbContext.Blogs.FirstOrDefault(b => b.Title == blogTitle);
            existingBlog.ShouldNotBeNull();

            var newVote = new VoteDto
            {
                AuthorId = voteAuthorId,
                CreationDate = DateTime.Parse(voteCreationDate),
                Value = voteValue
            };

            // Act
            var removeVoteResult = controller.RemoveVote(Convert.ToInt32(existingBlog.Id), voteAuthorId).Result as ObjectResult;
            var updatedBlog = dbContext.Blogs.FirstOrDefault(b => b.Title == blogTitle);

            // Assert - Response and Database
            removeVoteResult.ShouldNotBeNull();
            removeVoteResult.StatusCode.ShouldBe(200);

            Assert.Empty(updatedBlog.Votes);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogDto
            {
                Description = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new BlogDto
            {
                Id = -2,
                Title = "Naslov",
                Description = "Naslov je lud."
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BlogDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.Title.ShouldBe(updatedEntity.Title);
            result.Description.ShouldBe(updatedEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == "Naslov");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.Blogs.FirstOrDefault(i => i.Title == "Title");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogDto
            {
                Id = -1000,
                Title = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = (OkResult)controller.Delete(-1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Blogs.FirstOrDefault(i => i.Id == -1);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static BlogController CreateController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>(), scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
