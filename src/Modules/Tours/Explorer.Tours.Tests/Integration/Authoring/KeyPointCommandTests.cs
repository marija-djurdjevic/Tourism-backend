using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public void UpdatesKeyPoint()
        {
           
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var existingEntity = dbContext.KeyPoints.FirstOrDefault();
            existingEntity.ShouldNotBeNull();

            dbContext.Entry(existingEntity).State = EntityState.Detached;

            var updatedEntity = new KeyPointDto
            {
                
                Name = "Updated Name",  
                Description = "Updated Description",  
                Longitude = existingEntity.Coordinates.Longitude,
                Latitude = existingEntity.Coordinates.Latitude,
                ImagePath = existingEntity.ImagePath
               
            };

            
            var result = ((ObjectResult)controller.UpdateKeyPoint((int)existingEntity.Id, updatedEntity).Result)?.Value as KeyPointDto;

           
            result.ShouldNotBeNull();
            
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);

           
            var storedEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Id == existingEntity.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Name.ShouldBe(updatedEntity.Name);
            storedEntity.Description.ShouldBe(updatedEntity.Description);
        }

        [Fact]
        public void DeletesKeyPoint()
        {
           
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var existingEntity = dbContext.KeyPoints.FirstOrDefault();
            existingEntity.ShouldNotBeNull();

            var result = controller.DeleteKeyPoint((int)existingEntity.Id);

            var statusCodeResult = result as StatusCodeResult;
            statusCodeResult.ShouldNotBeNull();
            statusCodeResult.StatusCode.ShouldBe(204);

            var deletedEntity = dbContext.KeyPoints.FirstOrDefault(i => i.Id == existingEntity.Id);
            deletedEntity.ShouldBeNull();
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
