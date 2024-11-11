﻿using Explorer.API.Controllers.Tourist;
using Explorer.API.Controllers.Tourist.Execution;
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

        private static TourSessionController CreateController(IServiceScope scope)
        {
            return new TourSessionController(scope.ServiceProvider.GetRequiredService<ITourSessionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }






    }
}





