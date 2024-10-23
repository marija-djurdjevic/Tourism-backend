using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourEquipmentCommandTests:BaseToursIntegrationTest
    {
        public TourEquipmentCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourEquipmentDto
            {
                EquipmentId = 5,
                TourId = 5
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourEquipmentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldNotBe(0);
            result.EquipmentId.ShouldNotBe(0);

            // Assert - Database
            var storedEntity = dbContext.TourEquipment.FirstOrDefault(i => i.TourId == newEntity.TourId && i.EquipmentId==newEntity.EquipmentId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.TourEquipment.FirstOrDefault(i => i.Id == -3);
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

        private static TourEquipmentController CreateController(IServiceScope scope)
        {
            return new TourEquipmentController(scope.ServiceProvider.GetRequiredService<ITourEquipmentService>(), scope.ServiceProvider.GetRequiredService<IEquipmentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
