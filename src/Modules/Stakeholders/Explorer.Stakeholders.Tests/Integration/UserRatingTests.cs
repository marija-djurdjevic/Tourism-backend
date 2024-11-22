using TouristController = Explorer.API.Controllers.Tourist;
using adminController = Explorer.API.Controllers.Administrator; // Include both namespaces
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Explorer.Stakeholders.Infrastructure.Database;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class UserRatingTests : BaseStakeholdersIntegrationTest
    {
        public UserRatingTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Successfully_creates_rating()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var controller = CreateController<TouristController.UserRatingController>(scope, "Tourist", "-21", "turista1@gmail.com"); // For tourist role
            var ratingDto = new UserRatingDto
            {
                Rating = 3,
                Comment = "Not too bad"
            };

            var result = ((ObjectResult)controller.Create(ratingDto).Result)?.Value as UserRatingDto;
            result.ShouldNotBeNull();
            result.Rating.ShouldBe(ratingDto.Rating);

            // Assert - Database
            var storedEntity = dbContext.UserRatings.FirstOrDefault(i => i.Comment == ratingDto.Comment);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Gets_all_ratings_successfully()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController<adminController.UserRatingController>(scope, "Administrator", "-1", "admin@gmail.com");

            // Act
            var result = (ObjectResult)controller.GetAll().Result;

            // Assert
            result.StatusCode.ShouldBe(200);
            var ratings = result.Value as List<UserRatingDto>;
            ratings.ShouldNotBeNull();
            ratings.ShouldNotBeEmpty();
            ratings.Count.ShouldBe(3);
        }

        [Fact]
        public void Fails_to_create_rating_with_invalid_rating()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController<TouristController.UserRatingController>(scope, "Tourist", "-21", "turista1@gmail.com"); // For tourist role
            var invalidRatingDto = new UserRatingDto
            {
                Rating = 0, // Invalid rating
                Comment = "Invalid rating"
            };

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() =>
            {
                var result = controller.Create(invalidRatingDto).Result; // This line should throw
            });

            // Assert
            exception.Message.ShouldBe("Rating must be between 1 and 5.");
        }




        private static TController CreateController<TController>(IServiceScope scope, string role, string userId = "-1", string username = "user1") where TController : ControllerBase
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim("id", userId),
                new Claim("username", username),
                new Claim(ClaimTypes.Role, role)
            }, "TestAuth"));

            // Simulate user being authenticated
            var httpContext = new DefaultHttpContext { User = user };
            var controller = (TController)ActivatorUtilities.CreateInstance(scope.ServiceProvider, typeof(TController));
            controller.ControllerContext.HttpContext = httpContext;

            return controller;
        }


    }
}