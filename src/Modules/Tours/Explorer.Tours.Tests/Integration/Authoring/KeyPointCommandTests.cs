using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Authoring
{
    [Collection("Sequential")]
    public class KeyPointCommandTests : BaseToursIntegrationTest
    {
        public KeyPointCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new KeyPointDto
            {
                Id = 11,
                TourIds = new List<int> { -2 },
                Name = "Test",
                Description = "desc test",
                Longitude = 20,
                Latitude = 25,
                ImagePath = "path test",
                Status = KeyPointDto.KeyPointStatus.Pending
                
            };
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as KeyPointDto;
            //Assert response 
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);
            //Assert database
            var storedEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new KeyPointDto
            {
                Description = "Test",
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        private static KeyPointController CreateController(IServiceScope scope)
        {
            return new KeyPointController(scope.ServiceProvider.GetRequiredService<IKeyPointService>(), scope.ServiceProvider.GetRequiredService<IPublishRequestService>(), scope.ServiceProvider.GetRequiredService<INotificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }

}
