using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class ObjectCommandTests : BaseToursIntegrationTest
    {
        public ObjectCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates_object()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new ObjectDto
            {
                Name = "Test Object",
                Description = "Test description for object",
                ImageId = -1,
                Category = ObjectDto.ObjectCategory.WC

            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ObjectDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            // Assert - Database
            var storedEntity = dbContext.Object.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }



        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ObjectDto
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
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new ObjectDto
            {
                Id = -1,
                Name = "wce",
                Description = "Voda ili druga tečnost koja hidrira. Preporuka je pola litre tečnosti na sat vremena umerene aktivnosti po umerenoj temperaturi.",
                ImageId=-1,
                Category = (ObjectDto.ObjectCategory)ObjectCategory.WC


            };

            // Act
            var result = ((ObjectResult)controller.Update( updatedEntity).Result)?.Value as ObjectDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);

            // Assert - Database
            var storedEntity = dbContext.Object.FirstOrDefault(i => i.Name == "wce");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ObjectDto
            {
                Id = -1000,
                Name = "wce",
                Description = "Voda ili druga tečnost koja hidrira. Preporuka je pola litre tečnosti na sat vremena umerene aktivnosti po umerenoj temperaturi.",
                ImageId = -1,
                Category = (ObjectDto.ObjectCategory)ObjectCategory.WC

            };

            // Act
            var result = (ObjectResult)controller.Update( updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }



        private static ObjectController CreateController(IServiceScope scope)
        {
            return new ObjectController(scope.ServiceProvider.GetRequiredService<IObjectService>(), scope.ServiceProvider.GetRequiredService<IPublishRequestService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
