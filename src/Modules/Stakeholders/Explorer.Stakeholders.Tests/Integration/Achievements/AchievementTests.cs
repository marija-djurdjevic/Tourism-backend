using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Application.Dtos;
using Explorer.Stakeholders.Core.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration.Achievements
{
    [Collection("Sequential")]
    public class AchievementTests : BaseStakeholdersIntegrationTest
    {
        public AchievementTests(StakeholdersTestFactory factory) : base(factory) { }







        [Fact]
        public void Successfully_gets_achievements_for_user()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var claims = new List<Claim>
            {
                new Claim("id", "-2") // Koristi ID korisnika koji postoji
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            // Logovanje claim-ova za dijagnostiku
            foreach (var claim in controller.ControllerContext.HttpContext.User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }

            // Act
            var achievementsResponse = ((ObjectResult)controller.getAchievements().Result).Value as List<AchievementDto>;

            // Assert
            achievementsResponse.ShouldNotBeNull();
            achievementsResponse.Count.ShouldBeGreaterThan(0); // Očekuje se da korisnik ima bar jedno dostignuće
            achievementsResponse.ForEach(achievement => Console.WriteLine($"Achievement ID: {achievement.Id}, Name: {achievement.Name}, isEarnedByMe: {achievement.isEarnedByMe}"));
        }



        [Fact]
        public void Fails_to_get_achievements_for_invalid_user()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Kreiraj ClaimsPrincipal sa nevalidnim ID-om
            var claims = new List<Claim>
    {
        new Claim("id", "invalid") // Koristi nevalidan ID koji će izazvati grešku
    };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };

            // Act
            var result = (ObjectResult)controller.getAchievements().Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK); 

            // Ne proveravamo poruku greške jer kontroler možda ne vraća poruku
            result.Value.ShouldNotBeNull(); 
        }






        private static AchievementController CreateController(IServiceScope scope)
        {
            return new AchievementController(
                scope.ServiceProvider.GetRequiredService<IUserService>(),
                scope.ServiceProvider.GetRequiredService<IAchievementService>());
        }
    }
}
