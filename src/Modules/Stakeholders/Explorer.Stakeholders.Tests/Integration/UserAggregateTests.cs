using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.API.Controllers;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Shouldly;
using UserController = Explorer.API.Controllers.Tourist.Identity.UserController;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class UserAggregateTests : BaseStakeholdersIntegrationTest
    {
        public UserAggregateTests(StakeholdersTestFactory factory) : base(factory) { }

        [Theory]
        [InlineData("-3", 19.873495, 45.236847)]
        public void SetLocation(string userId, float longitude, float latitude)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope,userId);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            //act
            LocationDto locationDto = new LocationDto();
            locationDto.Longitude = longitude;
            locationDto.Latitude = latitude;
            var result = (ObjectResult)controller.SetTouristLocation(locationDto).Result;

            // assert - response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);

            // assert - database
            //var storedentity = dbContext.Users.firstordefault(t => t.id == tourid);
            //storedentity.shouldnotbenull();
            //storedentity.status.shouldbe(expectedstatus);
        }

        [Theory]
        [InlineData("-2", 19.873495, 45.236847)]
        public void GetLocation(string userId, float longitude, float latitude)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope,userId);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            //act
            LocationDto locationDto = new LocationDto();
            locationDto.Longitude = longitude;
            locationDto.Latitude = latitude;
            var result = (ObjectResult)controller.SetTouristLocation(locationDto).Result;

            // assert - response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // assert - database
            var user = dbContext.Users.FirstOrDefault(u => u.Id.ToString()==userId);
            user.ShouldNotBeNull();
            user.Location.Longitude.ShouldBe(longitude);
            user.Location.Latitude.ShouldBe(latitude);
        }

        private static UserController CreateController(IServiceScope scope,string userId)
        {
            return new UserController(scope.ServiceProvider.GetRequiredService<IUserService>())
            {
                ControllerContext = BuildContext(userId)
            };
        }
    }
}
