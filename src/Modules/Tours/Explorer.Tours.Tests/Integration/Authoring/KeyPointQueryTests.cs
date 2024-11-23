using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.API.Public.Execution;

namespace Explorer.Tours.Tests.Integration.Authoring
{
    [Collection("Sequential")]
    public class KeyPointQueryTests : BaseToursIntegrationTest
    {
        public KeyPointQueryTests(ToursTestFactory factory) : base(factory) { }
        [Fact]
        public void Retrives_all()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            //Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<KeyPointDto>;
            //Assert 
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(4);
            result.TotalCount.ShouldBe(4);
        }
        public static KeyPointController CreateController(IServiceScope scope)
        {
            return new KeyPointController(scope.ServiceProvider.GetRequiredService<IKeyPointService>(), scope.ServiceProvider.GetRequiredService<IPublishRequestService>(), scope.ServiceProvider.GetRequiredService<INotificationService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
