using Explorer.Tours.API.Dtos.TourLifecycleDtos;
using Explorer.Tours.API.Dtos;
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
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Public.Shopping;
using FluentResults;

namespace Explorer.Tours.Tests.Integration.Execution.Tours
{
    [Collection("Sequential")]
    public class TouristTests : BaseToursIntegrationTest
    {
        public TouristTests(ToursTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData(42.702882, 23.329503, 30.0, 1)]
        [InlineData(45.267135, 19.833548, 500.0, 2)]
        public void Tour_Search_By_Location_Success(double latitude, double longitude, double distance, int foundNum)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = (ObjectResult)controller.SearchTours(new SearchByDistanceDto(latitude, longitude, distance)).Result;
            var foundList = (result.Value as List<TourDto>);

            foundList.ShouldNotBeNull();
            foundList.Count.ShouldBe(foundNum);
        }

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<IShoppingService>(), scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<IKeyPointService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
