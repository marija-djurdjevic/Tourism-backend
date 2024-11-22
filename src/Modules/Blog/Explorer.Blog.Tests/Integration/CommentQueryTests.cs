using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;


namespace Explorer.Blog.Tests.Integration
{
    //[Collection("Sequential")]
    //public class CommentQueryTests : BaseBlogIntegrationTest
    //{
    //    public CommentQueryTests(BlogTestFactory factory) : base(factory) { }

    //    [Fact]
    //    public void Retrieves_all()
    //    {
    //        // Arrange
    //        using var scope = Factory.Services.CreateScope();
    //        var controller = CreateController(scope);

    //        // Act
    //        var result = ((ObjectResult)controller.GetAllByBlogId(1).Result)?.Value as PagedResult<CommentDto>;

    //        // Assert
    //        result.ShouldNotBeNull();
    //        result.Results.Count.ShouldBe(3);
    //        result.TotalCount.ShouldBe(3);
    //    }

    //    private static CommentController CreateController(IServiceScope scope)
    //    {
    //        return new CommentController(scope.ServiceProvider.GetRequiredService<ICommentService>())
    //        {
    //            ControllerContext = BuildContext("-1")
    //        };
    //    }
    //}
}
