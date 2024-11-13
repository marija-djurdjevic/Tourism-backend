using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration
{
    public class ClubsCommandTests : BaseToursIntegrationTest
    {
        public ClubsCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new ClubDto
            {
                Name = "nesvrstani",
                Description = "jaki momci titovi",
                ImageId = -1,
            };

            // Act
            //var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubDto;
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubDto;
            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            // Assert - Database
            var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubDto
            {
                Description = "Test los"
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
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new ClubDto
            {
                Id = -1,
                OwnerId = 0,
                Name = "klub amatera",
                Description = "jos bolji ljudi",
                ImageId = -1
            };

            var result = controller.Update(updatedEntity).Result;

            // Assert - Response
            if (result is ForbidResult)
            {
                result.ShouldBeOfType<ForbidResult>();
            }
            else if (result is ObjectResult objectResult)
            {
                var updatedClub = objectResult.Value as ClubDto;
                updatedClub.ShouldNotBeNull();
                updatedClub.Id.ShouldBe(-1);
                updatedClub.Name.ShouldBe(updatedEntity.Name);
                updatedClub.Description.ShouldBe(updatedEntity.Description);

                // Assert - Database
                var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Description == "jos bolji ljudi");
                storedEntity.ShouldNotBeNull();
                storedEntity.Description.ShouldBe(updatedEntity.Description);
                var oldEntity = dbContext.Clubs.FirstOrDefault(i => i.Description == "najbolji ljudi");
                oldEntity.ShouldBeNull();
            }
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubDto
            {
                Id = -1000,
                OwnerId = 0,
                Name = "klub amatera",
                Description = "o joko joko",
                ImageId = -1
            };

            // Act
            var result = controller.Update(updatedEntity).Result;

            // Assert
            if (result is ForbidResult)
            {
                result.ShouldBeOfType<ForbidResult>();
            }
            else if (result is ObjectResult objectResult)
            {
                objectResult.ShouldNotBeNull();
                objectResult.StatusCode.ShouldBe(404);
            }

        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = controller.Delete(-3);

            // Assert - Response
            if (result is ForbidResult)
            {
                result.ShouldBeOfType<ForbidResult>();
            }
            else if (result is OkResult okResult)
            {
                okResult.ShouldNotBeNull();
                okResult.StatusCode.ShouldBe(200);

                // Assert - Database
                var storedCourse = dbContext.Clubs.FirstOrDefault(i => i.Id == -3);
                storedCourse.ShouldBeNull();
            }
        }
        /*
        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = controller.Delete(-1000);

            // Assert
            if (result is ForbidResult)
            {
                result.ShouldBeOfType<ForbidResult>();
            }
            else if (result is NotFoundResult notFoundResult)
            {
                notFoundResult.ShouldNotBeNull();
                notFoundResult.StatusCode.ShouldBe(404);
            }

        }
        */
        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = controller.Delete(-1000);

            // Assert
            if (result is ForbidResult)
            {
                result.ShouldBeOfType<ForbidResult>();
            }
            else if (result is ObjectResult objectResult)
            {
                objectResult.ShouldNotBeNull();
                objectResult.StatusCode.ShouldBe(404);
            }
        }

        private static ClubController CreateController(IServiceScope scope)
        {
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
