using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Authoring;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Explorer.Tours.API.Dtos.TourLifecycleDtos.TourDto;

namespace Explorer.Tours.Tests.Integration.Authoring
{
    [Collection("Sequential")]
    public class TourCommandTests : BaseToursIntegrationTest
    {
        public TourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourDto
            {
                Id = 1,
                AuthorId = 1,
                Name = "Test",
                Description = "desc test",
                Difficulty = 0,
                Tags = "#hiking,#adventure,#test",
                Price = 1000,
                Status = 0,
                AverageScore = 0,
                ArchivedAt = DateTime.UtcNow,
                PublishedAt = DateTime.UtcNow,
                KeyPoints = new List<KeyPointDto>(),
                TransportInfo = new TransportInfoDto()

        };

            //Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

            //Assert response 
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            //Assert database
            var storedEntity = dbContext.Tour.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Publishes_tour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedTour = new TourDto { Id = -1, AuthorId = 1, Description = "sdadsa", AverageScore = 0, Price = 0, Tags = "sss", Name = "Publish Test Tour", Status = TourStatus.Archived };

            // Act
            var result = ((ObjectResult)controller.Publish(updatedTour).Result)?.Value as TourDto;

            // Assert
            result.ShouldNotBeNull();
            result.Status.ShouldBe(TourStatus.Published);

            //Assert - database
            var storedEntity = dbContext.Tour.FirstOrDefault(i => i.Id == -1);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(TourStatus.Published.ToString());

        }

        [Fact]
        public void Archives_tour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedTour = new TourDto { Id = -2, AuthorId = 1, Description = "sdadsa", AverageScore = 0, Price = 0, Tags = "sss", Name = "Archive Test Tour", Status = TourStatus.Published };

            // Act
            var result = ((ObjectResult)controller.Archive(updatedTour).Result)?.Value as TourDto;

            // Assert
            result.ShouldNotBeNull();
            result.Status.ShouldBe(TourStatus.Archived);
            //Assert - database
            var storedEntity = dbContext.Tour.FirstOrDefault(i => i.Id == -2);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(TourStatus.Archived.ToString());

        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourDto
            {
                Description = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
