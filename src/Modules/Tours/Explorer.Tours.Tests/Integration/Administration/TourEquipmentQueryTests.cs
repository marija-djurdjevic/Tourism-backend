using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourEquipmentQueryTests : BaseToursIntegrationTest
    {
        public TourEquipmentQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            TourEquipmentController controller = CreateController(scope);

            // Act
            var result = (controller.GetEquipmentbyTourId(-1, 1, 10));

            // Assert
            result.ShouldNotBeNull();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.ShouldBe(200);

            var equipmentList = objectResult.Value as IEnumerable<object>;
            equipmentList.ShouldNotBeNull();
            equipmentList.Count().ShouldBe(2);
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
