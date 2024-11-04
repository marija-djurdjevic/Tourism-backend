using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourReviewCommandTests : BaseToursIntegrationTest
    {
        public TourReviewCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourReviewDto
            {
                Grade = 5,
                Comment = "Nice",
                TourId = -1,
                UserId = -1,
                Username = "Test",
                Images = new List<string>(),
                TourVisitDate = DateTime.UtcNow,
                TourReviewDate = DateTime.UtcNow,
                TourProgressPercentage = 50
            };
            newEntity.Images.Add("image1");

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Grade.ShouldBe(newEntity.Grade);
            result.Comment.ShouldBe(newEntity.Comment);
            result.TourId.ShouldBe(newEntity.TourId);
            result.UserId.ShouldBe(newEntity.UserId);
            result.Username.ShouldBe(newEntity.Username);
            result.Images.ShouldBe(newEntity.Images);
            result.TourVisitDate.ShouldBe(newEntity.TourVisitDate);
            result.TourReviewDate.ShouldBe(newEntity.TourReviewDate);

            // Assert - Database
            var storedEntity = dbContext.TourReview.FirstOrDefault(i => i.Grade == newEntity.Grade && i.Comment == newEntity.Comment && i.TourId == newEntity.TourId && i.UserId == newEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
       
        private static TourReviewController CreateController(IServiceScope scope)
        {
            return new TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>(),scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
