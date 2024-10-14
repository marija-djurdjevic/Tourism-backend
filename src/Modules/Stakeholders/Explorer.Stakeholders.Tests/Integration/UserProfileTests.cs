using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class UserProfileTests : BaseStakeholdersIntegrationTest
    {
        public UserProfileTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Successfully_gets_profile()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var profile = new UserProfileDto { Id = -1, FirstName="Any",LastName="Any" };

            // Act
            var profileResponse = ((ObjectResult)controller.Get((int)profile.Id).Result).Value as UserProfileDto;

            // Assert
            profileResponse.ShouldNotBeNull();
            profileResponse.Id.ShouldBe(-1);
            profileResponse.FirstName.ShouldBe("Ana");
            profileResponse.LastName.ShouldBe("Anić");


        }
        [Fact]
        public void Successfully_updates_profile()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var profile = new UserProfileDto { Id = -2, FirstName = "Marko", LastName = "Markovic", ImageURL = "marko.jpg", Biography= "Retired dreamer", Motto = "Live, laugh, love." };

            // Act
            var profileResponse = ((ObjectResult)controller.Update(profile).Result).Value as UserProfileDto;

            //Assert
            profileResponse.ShouldNotBeNull(); 
            profileResponse.Id.ShouldBe(-2); 
            profileResponse.FirstName.ShouldBe("Marko"); 
            profileResponse.LastName.ShouldBe("Markovic"); 
            profileResponse.ImageURL.ShouldBe("marko.jpg"); 
            profileResponse.Biography.ShouldBe("Retired dreamer"); 
            profileResponse.Motto.ShouldBe("Live, laugh, love."); 
        }

        private static UserProfileController CreateController(IServiceScope scope)
        {
            return new UserProfileController(scope.ServiceProvider.GetRequiredService<IUserProfileService>());
        }
    }
}
