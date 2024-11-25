using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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

namespace Explorer.Tours.Tests.Integration
{

    [Collection("Sequential")]
    public class ObjectQueryTest : BaseToursIntegrationTest
    {

        public ObjectQueryTest(ToursTestFactory factory) : base(factory) { }



        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ObjectDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
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
