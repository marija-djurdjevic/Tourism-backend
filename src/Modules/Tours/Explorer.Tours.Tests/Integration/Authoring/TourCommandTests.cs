using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Authoring;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Public.Authoring;
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

            var updatedTour = new TourDto { Id = -1, AuthorId = 1, Description = "sdadsa", AverageScore = 0, Price = 0, Tags = "sss", Name = "Publish Test Tour", Status = API.Dtos.TourLifecycleDtos.TourDto.TourStatus.Archived };


            updatedTour.KeyPoints = new List<KeyPointDto>();
            updatedTour.KeyPoints.Add(new KeyPointDto { Id = 100, Description = "kp1", ImagePath = "...", Latitude = 2.2, Longitude = 3.3, Name = "KP1", TourId = -1 });
            updatedTour.KeyPoints.Add(new KeyPointDto { Id = 200, Description = "kp2", ImagePath = "...", Latitude = 2.2, Longitude = 3.3, Name = "KP2", TourId = -1 });
            updatedTour.TransportInfo = new TransportInfoDto() { Distance = 2.0, Time = 100, Transport = TransportInfoDto.TransportType.Car }; 

            // Act
            var result = ((ObjectResult)controller.Publish(updatedTour).Result)?.Value as TourDto;

            // Assert
            result.ShouldNotBeNull();
            result.Status.ShouldBe(API.Dtos.TourLifecycleDtos.TourDto.TourStatus.Published);

            //Assert - database
            var storedEntity = dbContext.Tour.FirstOrDefault(i => i.Id == -1);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(API.Dtos.TourLifecycleDtos.TourDto.TourStatus.Published.ToString());

        }

        
        [Fact]
        public void Archives_tour()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedTour = new TourDto { Id = -2, AuthorId = 1, Description = "sdadsa", AverageScore = 0, Price = 0, Tags = "sss", Name = "Archive Test Tour", Status = API.Dtos.TourLifecycleDtos.TourDto.TourStatus.Published };

            // Act
            var result = ((ObjectResult)controller.Archive(updatedTour).Result)?.Value as TourDto;

            // Assert
            result.ShouldNotBeNull();
            result.Status.ShouldBe(API.Dtos.TourLifecycleDtos.TourDto.TourStatus.Archived);
            //Assert - database
            var storedEntity = dbContext.Tour.FirstOrDefault(i => i.Id == -2);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ToString().ShouldBe(API.Dtos.TourLifecycleDtos.TourDto.TourStatus.Archived.ToString());

        }

        [Fact]
        public void UpdateTransportInfo_SuccessfulUpdate()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            
            var tour = dbContext.Tour.AsNoTracking().FirstOrDefault(t => t.Id == 1);
            tour.ShouldNotBeNull(); 

            
            var originalDistance = tour.TransportInfo.Distance;
            var originalTime = tour.TransportInfo.Time;

           
            var transportInfoDto = new TransportInfoDto
            {
                Distance = originalDistance + 500, 
                Time = originalTime + 30           
            };

            // Act 
            var result = ((ObjectResult)controller.UpdateTransportInfo((int)tour.Id, transportInfoDto).Result)?.Value as bool?;

            // Assert - Response
            result.ShouldNotBeNull();
            result.ShouldBe(true);

            // Assert - Database
            var updatedTour = dbContext.Tour.FirstOrDefault(t => t.Id == tour.Id);
            updatedTour.ShouldNotBeNull();
            updatedTour.TransportInfo.Distance.ShouldBe(transportInfoDto.Distance);
            updatedTour.TransportInfo.Time.ShouldBe(transportInfoDto.Time);

          
            updatedTour.TransportInfo.Distance.ShouldNotBe(originalDistance);
            updatedTour.TransportInfo.Time.ShouldNotBe(originalTime);
        }

        [Fact]
        public void UpdateTransportInfo_Fails_InvalidId()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var invalidTourId = -1000; 
            var transportInfoDto = new TransportInfoDto
            {
                Distance = 1500,
                Time = 120
            };

            // Act
            var result = (ObjectResult)controller.UpdateTransportInfo(invalidTourId, transportInfoDto).Result;

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
