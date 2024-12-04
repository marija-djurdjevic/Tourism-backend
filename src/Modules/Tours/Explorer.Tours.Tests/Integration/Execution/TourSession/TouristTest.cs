using Explorer.API.Controllers.Tourist;
using Explorer.API.Controllers.Tourist.Execution;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Explorer.Tours.API.Dtos.TourSessionDtos.TourSessionDto;

namespace Explorer.Tours.Tests.Integration.Execution.TourSession
{

    [Collection("Sequential")]
    public class TouristTest :BaseToursIntegrationTest 
    {

        public TouristTest(ToursTestFactory factory) : base(factory) { }

        /*
        [Theory]
        [InlineData(-3, 45.2671, 19.8335, -21)]
        public void StartTourSucceeds(int tourId, double latitude, double longitude,int touristId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var claims = new List<Claim>
            {
                new Claim("personId", $"{touristId}")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var result = (ObjectResult)controller.StartTour(tourId, latitude, longitude, touristId).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }
        */
        [Theory]
        [InlineData(-3, 45.2671, 19.8335, -21)]
        public void StartTourSucceeds(int tourId, double latitude, double longitude, int touristId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Mock user claims to simulate logged-in user
            var claims = new List<Claim>
    {
        new Claim("personId", $"{touristId}")
    };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            // Create StartTourDto with test data
            var startTourDto = new StartTourDto
            {
                TourId = tourId,
                Latitude = latitude,
                Longitude = longitude
            };

            // Call the modified StartTour method and assert results
            var result = (ObjectResult)controller.StartTour(startTourDto).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }


        [Theory]
        [InlineData(-2, -21)]
        public void CompleteTourSucceeds(int sessionId, int touristId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var claims = new List<Claim>
            {
                new Claim("personId", $"{touristId}")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var result = (ObjectResult)controller.CompleteTour(sessionId).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        [Theory]
        [InlineData(-2, -21)]
        public void AbandonTourSucceeds(int sessionId, int touristId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var claims = new List<Claim>
            {
                new Claim("personId", $"{touristId}")
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var result = (ObjectResult)controller.AbandonTour(sessionId).Result;

            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }

        [Theory]
        [InlineData(-2, -21)]  
        public void UpdateLastActivitySucceeds(int sessionId, int userId)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Set up mock claims to simulate the authenticated user
            var claims = new List<Claim>
    {
        new Claim("personId", $"{userId}")
    };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            // Act
            var result = (ObjectResult)controller.UpdateLastActivity(sessionId).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldBe(true);
        }

        [Theory]
        [InlineData(-999, -21)]  // Use non-existent or invalid session ID
        public void UpdateLastActivityFails_NotFound(int sessionId, int userId)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var claims = new List<Claim>
    {
        new Claim("personId", $"{userId}")
    };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            // Act
            var result = (NotFoundObjectResult)controller.UpdateLastActivity(sessionId).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
            result.Value.ShouldBe(false);
        }

        [Theory]
        [InlineData(-2, -101, -21)]
        public void CompleteKeyPoint_Succeeds(long tourId, long keyPointId, int userId)
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Mock user identity
            var claims = new List<Claim> { new Claim("personId", $"{userId}") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = (ObjectResult)controller.CompleteKeyPoint(tourId, keyPointId).Result;

            // Assert
            result.ShouldNotBeNull("Expected a non-null result from CompleteKeyPoint.");
            result.StatusCode.ShouldBe(200, "Expected a 200 OK status code.");
            result.Value.ShouldBe(true, "Expected the action to return true.");
        }

        [Theory]
        [InlineData(-999, -101, -21)]  // Using a tourId that doesn't exist
        public void CompleteKeyPoint_Fails_WhenTourSessionNotFound(long tourId, long keyPointId, int userId)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Mock user identity
            var claims = new List<Claim> { new Claim("personId", $"{userId}") };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = (ObjectResult)controller.CompleteKeyPoint(tourId, keyPointId).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);  // Expecting BadRequest (400)
            result.Value.ShouldBe(false);  // The return value should be false
        }


        private static TourSessionController CreateController(IServiceScope scope)
        {
            return new TourSessionController(scope.ServiceProvider.GetRequiredService<ITourSessionService>(), scope.ServiceProvider.GetRequiredService<IEncounterService>(), scope.ServiceProvider.GetRequiredService<IEncounterExecutionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }






    }
}





